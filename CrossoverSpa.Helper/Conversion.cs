using CrossoverSpa.Entities;
using CrossoverSpa.ViewModel;
using CrossoverSpa.ViewModels;

namespace CrossoverSpa.Helper
{
    public static class Conversion
    {
        public static RoleViewModel ToVM(this Role obj)
        {
            if (obj == null) return null;
            return new RoleViewModel
            {
                Id = obj.Id,
                Name = obj.Name
            };
        }

        public static UserViewModel ToVM(this User obj)
        {
            if (obj == null) return null;
            return new UserViewModel
            {
                Id = obj.Id,
                Email = obj.Email,
                Password = "",
                RoleId = obj.RoleId,
                Role = obj.Role.ToVM()
            };
        }

        public static FeatureViewModel ToVM(this Feature obj)
        {
            if (obj == null) return null;
            return new FeatureViewModel
            {
                Id = obj.Id,
                Name = obj.Name,
                RouteUrl=obj.RouteUrl
                
            };
        }

        public static RoleFeatureViewModel ToVM(this RoleFeature obj)
        {
            if (obj == null) return null;
            return new RoleFeatureViewModel
            {
                Id = obj.Id,

                

                RoleId = obj.RoleId,
                Role = obj.Role.ToVM(),

                FeatureId = obj.FeatureId,
                Feature = obj.Feature.ToVM()
            };
        }


        public static EmployeeViewModel ToVM(this Employee obj)
        {
            if (obj == null) return null;
            return new EmployeeViewModel
            {
                Id = obj.Id,
                Name = obj.Name,
                Designation = obj.Designation,
                Department = obj.Department,
                AddedDate = obj.AddedDate,
                ModifiedDate = obj.ModifiedDate,

                UserId = obj.UserId,
                User = obj.User.ToVM(),

                AddedById = obj.AddedById,
                AddedBy = obj.AddedBy.ToVM(),

                ModifiedById = obj.ModifiedById,
                ModifiedBy = obj.ModifiedBy.ToVM()

            };
        }


        public static ContactViewModel ToVM(this Contact obj)
        {
            if (obj == null) return null;
            return new ContactViewModel
            {
                Id = obj.Id,
                Address = obj.Address,

                Mobile1 = obj.Mobile1,
                Mobile2 = obj.Mobile2,
                Mobile3 = obj.Mobile3,

                Phone1 = obj.Phone1,
                Phone2 = obj.Phone2,
                Phone3 = obj.Phone3,

                Email1 = obj.Email1,
                Email2 = obj.Email2
            };
        }

        public static EmployeeContactViewModel ToVM(this EmployeeContact obj)
        {
            if (obj == null) return null;
            return new EmployeeContactViewModel
            {
                Id = obj.Id,

               

                EmployeeId = obj.EmployeeId,
                Employee = obj.Employee.ToVM(),

                ContactId = obj.ContactId,
                Contact = obj.Contact.ToVM()
            };
        }
    }
}
