using MediFinder.Data;
using MediFinder.Models;
using MediFinder.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MediFinder.Services;

public class DoctorService(ApplicationDbContext db) : IDoctorService
{
    private const int PageSize = 9;

    public async Task<HomeViewModel> GetHomeAsync()
    {
        var featuredDoctors = await DoctorsWithIncludes()
            .OrderByDescending(doctor => doctor.AverageRating)
            .ThenByDescending(doctor => doctor.ReviewCount)
            .Take(3)
            .ToListAsync();

        return new HomeViewModel
        {
            Areas = await GetAreaOptionsAsync(),
            Cities = await GetCityOptionsAsync(),
            Specializations = await GetSpecializationOptionsAsync(),
            FeaturedDoctors = featuredDoctors.Select(ToListItem).ToList()
        };
    }

    public async Task<SearchResultsViewModel> SearchAsync(SearchOptionsViewModel search)
    {
        search.Page = Math.Max(1, search.Page);
        search.SortBy = string.IsNullOrWhiteSpace(search.SortBy) ? "rating" : search.SortBy;

        var query = DoctorsWithIncludes();

        if (!string.IsNullOrWhiteSpace(search.City))
        {
            query = query.Where(doctor => doctor.Clinics.Any(clinic => clinic.Area.City == search.City));
        }

        if (!string.IsNullOrWhiteSpace(search.Area))
        {
            query = query.Where(doctor => doctor.Clinics.Any(clinic => clinic.Area.Slug == search.Area));
        }

        if (!string.IsNullOrWhiteSpace(search.Specialization))
        {
            query = query.Where(doctor => doctor.DoctorSpecializations.Any(item => item.Specialization.Slug == search.Specialization));
        }

        if (!string.IsNullOrWhiteSpace(search.Keyword))
        {
            var keyword = search.Keyword.Trim();
            query = query.Where(doctor =>
                doctor.FullName.Contains(keyword) ||
                doctor.Qualification.Contains(keyword) ||
                doctor.Bio.Contains(keyword) ||
                doctor.Clinics.Any(clinic => clinic.Name.Contains(keyword)));
        }

        query = search.SortBy switch
        {
            "experience" => query.OrderByDescending(doctor => doctor.ExperienceYears),
            "fee" => query.OrderBy(doctor => doctor.Clinics.Min(clinic => clinic.ConsultationFee)),
            _ => query.OrderByDescending(doctor => doctor.AverageRating).ThenByDescending(doctor => doctor.ReviewCount)
        };

        var total = await query.CountAsync();
        var doctors = await query
            .Skip((search.Page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();

        return new SearchResultsViewModel
        {
            Search = search,
            Areas = await GetAreaOptionsAsync(),
            Cities = await GetCityOptionsAsync(),
            Specializations = await GetSpecializationOptionsAsync(),
            Doctors = doctors.Select(ToListItem).ToList(),
            TotalDoctors = total
        };
    }

    public async Task<DoctorProfileViewModel?> GetProfileAsync(string slug)
    {
        var doctor = await DoctorsWithIncludes()
            .Include(item => item.Reviews.Where(review => review.IsApproved))
            .FirstOrDefaultAsync(item => item.Slug == slug);

        if (doctor is null)
        {
            return null;
        }

        return new DoctorProfileViewModel
        {
            DoctorId = doctor.DoctorId,
            FullName = doctor.FullName,
            Slug = doctor.Slug,
            Qualification = doctor.Qualification,
            ExperienceYears = doctor.ExperienceYears,
            Bio = doctor.Bio,
            ProfileImageUrl = doctor.ProfileImageUrl,
            AverageRating = doctor.AverageRating,
            ReviewCount = doctor.ReviewCount,
            Specializations = doctor.DoctorSpecializations.Select(item => item.Specialization.Name).Order().ToList(),
            Clinics = doctor.Clinics.Select(clinic => new ClinicViewModel
            {
                Name = clinic.Name,
                Address = clinic.Address,
                PhoneNumber = clinic.PhoneNumber,
                Area = clinic.Area.Name,
                City = clinic.Area.City,
                ConsultationFee = clinic.ConsultationFee
            }).OrderBy(clinic => clinic.City).ThenBy(clinic => clinic.Area).ToList(),
            Availability = doctor.Availabilities.Select(item => $"{item.DayOfWeek}: {item.StartTime:hh\\:mm} - {item.EndTime:hh\\:mm}").ToList(),
            Reviews = doctor.Reviews.OrderByDescending(review => review.CreatedAt).Select(review => new ReviewListItemViewModel
            {
                ReviewerName = review.ReviewerName,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt
            }).ToList(),
            ReviewForm = new ReviewFormViewModel { DoctorId = doctor.DoctorId }
        };
    }

    private IQueryable<Doctor> DoctorsWithIncludes()
    {
        return db.Doctors
            .AsNoTracking()
            .Where(doctor => doctor.IsActive)
            .Include(doctor => doctor.DoctorSpecializations)
            .ThenInclude(item => item.Specialization)
            .Include(doctor => doctor.Clinics.Where(clinic => clinic.IsActive))
            .ThenInclude(clinic => clinic.Area)
            .Include(doctor => doctor.Availabilities);
    }

    private static DoctorListItemViewModel ToListItem(Doctor doctor)
    {
        var clinic = doctor.Clinics.OrderBy(item => item.ConsultationFee).FirstOrDefault();

        return new DoctorListItemViewModel
        {
            DoctorId = doctor.DoctorId,
            FullName = doctor.FullName,
            Slug = doctor.Slug,
            Qualification = doctor.Qualification,
            ExperienceYears = doctor.ExperienceYears,
            Bio = doctor.Bio,
            ProfileImageUrl = doctor.ProfileImageUrl,
            AverageRating = doctor.AverageRating,
            ReviewCount = doctor.ReviewCount,
            StartingFee = clinic?.ConsultationFee ?? 0,
            PrimaryClinic = clinic?.Name ?? "Clinic details available soon",
            PrimaryArea = clinic is null ? "Area available soon" : $"{clinic.Area.Name}, {clinic.Area.City}",
            Specializations = doctor.DoctorSpecializations.Select(item => item.Specialization.Name).Order().ToList()
        };
    }

    private async Task<IReadOnlyList<SelectOptionViewModel>> GetAreaOptionsAsync()
    {
        return await db.Areas.AsNoTracking()
            .Where(area => area.IsActive)
            .OrderBy(area => area.City)
            .ThenBy(area => area.Name)
            .Select(area => new SelectOptionViewModel { Value = area.Slug, Label = area.Name + ", " + area.City })
            .ToListAsync();
    }

    private async Task<IReadOnlyList<SelectOptionViewModel>> GetCityOptionsAsync()
    {
        return await db.Areas.AsNoTracking()
            .Where(area => area.IsActive)
            .Select(area => area.City)
            .Distinct()
            .OrderBy(city => city)
            .Select(city => new SelectOptionViewModel { Value = city, Label = city })
            .ToListAsync();
    }

    private async Task<IReadOnlyList<SelectOptionViewModel>> GetSpecializationOptionsAsync()
    {
        return await db.Specializations.AsNoTracking()
            .Where(specialization => specialization.IsActive)
            .OrderBy(specialization => specialization.Name)
            .Select(specialization => new SelectOptionViewModel { Value = specialization.Slug, Label = specialization.Name })
            .ToListAsync();
    }
}
