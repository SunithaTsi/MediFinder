using MediFinder.Models;
using Microsoft.EntityFrameworkCore;

namespace MediFinder.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await db.Database.EnsureCreatedAsync();

        if (await db.Doctors.AnyAsync())
        {
            return;
        }

        var cardiology = new Specialization { Name = "Cardiology", Slug = "cardiology", Description = "Heart and vascular care." };
        var dermatology = new Specialization { Name = "Dermatology", Slug = "dermatology", Description = "Skin, hair, and nail care." };
        var pediatrics = new Specialization { Name = "Pediatrics", Slug = "pediatrics", Description = "Medical care for infants, children, and teens." };
        var orthopedics = new Specialization { Name = "Orthopedics", Slug = "orthopedics", Description = "Bone, joint, and spine care." };

        var adyar = new Area { Name = "Adyar", City = "Chennai", State = "Tamil Nadu", PinCode = "600020", Slug = "adyar-chennai" };
        var annaNagar = new Area { Name = "Anna Nagar", City = "Chennai", State = "Tamil Nadu", PinCode = "600040", Slug = "anna-nagar-chennai" };
        var indiranagar = new Area { Name = "Indiranagar", City = "Bengaluru", State = "Karnataka", PinCode = "560038", Slug = "indiranagar-bengaluru" };

        var doctors = new[]
        {
            new Doctor
            {
                FullName = "Dr. Ananya Raman",
                Slug = "dr-ananya-raman-cardiologist-chennai",
                Qualification = "MD, DM Cardiology",
                ExperienceYears = 14,
                Bio = "Focused on preventive cardiology, hypertension management, and long-term heart health planning.",
                ProfileImageUrl = "https://images.unsplash.com/photo-1559839734-2b71ea197ec2?auto=format&fit=crop&w=600&q=80",
                AverageRating = 4.8m,
                ReviewCount = 3
            },
            new Doctor
            {
                FullName = "Dr. Vikram Menon",
                Slug = "dr-vikram-menon-dermatologist-chennai",
                Qualification = "MD Dermatology",
                ExperienceYears = 9,
                Bio = "Treats clinical dermatology concerns and offers practical plans for acne, eczema, and pigmentation.",
                ProfileImageUrl = "https://images.unsplash.com/photo-1622253692010-333f2da6031d?auto=format&fit=crop&w=600&q=80",
                AverageRating = 4.6m,
                ReviewCount = 2
            },
            new Doctor
            {
                FullName = "Dr. Meera Iyer",
                Slug = "dr-meera-iyer-pediatrician-bengaluru",
                Qualification = "MD Pediatrics",
                ExperienceYears = 12,
                Bio = "Known for calm pediatric consultations, vaccination guidance, and child nutrition support.",
                ProfileImageUrl = "https://images.unsplash.com/photo-1594824476967-48c8b964273f?auto=format&fit=crop&w=600&q=80",
                AverageRating = 4.9m,
                ReviewCount = 4
            },
            new Doctor
            {
                FullName = "Dr. Arjun Nair",
                Slug = "dr-arjun-nair-orthopedic-surgeon-chennai",
                Qualification = "MS Orthopedics",
                ExperienceYears = 16,
                Bio = "Specializes in sports injuries, joint pain, and evidence-led rehabilitation plans.",
                ProfileImageUrl = "https://images.unsplash.com/photo-1537368910025-700350fe46c7?auto=format&fit=crop&w=600&q=80",
                AverageRating = 4.7m,
                ReviewCount = 3
            }
        };

        doctors[0].DoctorSpecializations.Add(new DoctorSpecialization { Doctor = doctors[0], Specialization = cardiology });
        doctors[1].DoctorSpecializations.Add(new DoctorSpecialization { Doctor = doctors[1], Specialization = dermatology });
        doctors[2].DoctorSpecializations.Add(new DoctorSpecialization { Doctor = doctors[2], Specialization = pediatrics });
        doctors[3].DoctorSpecializations.Add(new DoctorSpecialization { Doctor = doctors[3], Specialization = orthopedics });

        doctors[0].Clinics.Add(new Clinic { Area = adyar, Name = "Harbor Heart Clinic", Address = "22 River View Road, Adyar", PhoneNumber = "+91 98765 10001", Email = "care@harborheart.example", ConsultationFee = 900 });
        doctors[0].Clinics.Add(new Clinic { Area = annaNagar, Name = "Northside Cardiac Centre", Address = "11 Park Avenue, Anna Nagar", PhoneNumber = "+91 98765 10002", ConsultationFee = 1000 });
        doctors[1].Clinics.Add(new Clinic { Area = annaNagar, Name = "Clear Skin Studio", Address = "4 Second Avenue, Anna Nagar", PhoneNumber = "+91 98765 20001", ConsultationFee = 650 });
        doctors[2].Clinics.Add(new Clinic { Area = indiranagar, Name = "Little Steps Clinic", Address = "80 CMH Road, Indiranagar", PhoneNumber = "+91 98765 30001", ConsultationFee = 700 });
        doctors[3].Clinics.Add(new Clinic { Area = adyar, Name = "Motion Orthopedic Care", Address = "9 Besant Avenue, Adyar", PhoneNumber = "+91 98765 40001", ConsultationFee = 850 });

        doctors[0].Reviews.Add(new Review { ReviewerName = "Priya S.", Rating = 5, Comment = "Clear diagnosis and very patient with follow-up questions.", IsApproved = true });
        doctors[0].Reviews.Add(new Review { ReviewerName = "Ramesh K.", Rating = 5, Comment = "Good preventive advice and practical medicine schedule.", IsApproved = true });
        doctors[1].Reviews.Add(new Review { ReviewerName = "Nisha P.", Rating = 4, Comment = "Treatment plan was simple and easy to follow.", IsApproved = true });
        doctors[2].Reviews.Add(new Review { ReviewerName = "Asha M.", Rating = 5, Comment = "Wonderful with children and explains everything calmly.", IsApproved = true });
        doctors[3].Reviews.Add(new Review { ReviewerName = "Karthik V.", Rating = 5, Comment = "Helped me understand my knee pain and recovery plan.", IsApproved = true });

        db.Doctors.AddRange(doctors);
        await db.SaveChangesAsync();
    }
}
