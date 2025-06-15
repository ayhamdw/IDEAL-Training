namespace ProjectBase.Core.Enums
{
    public class GeneralEnums
    {
        public enum FileEnum
        {
            Image = 1,
        }

        public enum LanguageEnum
        {
            Arabic = 1,
            English = 2,           
            Russian = 4,
            French = 3,
            Urdu = 5,
        }

        public enum StatusEnum
        {
            Active = 1,
            Deleted = 2,
            Deactive = 3,
        }

        public enum AppointmentEnum
        {
            Active = 1,
            Deleted = 2,
            Cancel=3,
        }

        public enum ServiceTypeEnum
        {
            Service = 1,
            MedicalRecords = 2,
        }

        public enum AWSFileTypeEnum
        {
            UserImage = 1,
        }
        public enum MasterLookupCodeEnums
        {
            Languages = 1,
            Gender = 2,
            Pagination = 3,
        }       
    }
}
