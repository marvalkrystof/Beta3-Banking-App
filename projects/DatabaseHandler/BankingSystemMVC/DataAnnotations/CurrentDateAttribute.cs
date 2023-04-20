using System.ComponentModel.DataAnnotations;

namespace BankingSystemMVC.DataAnnotations
    {
    /// <summary>
    /// Custom data annotations for model checking
    /// </summary>
        public class CurrentDateAttribute : ValidationAttribute
        {
        /// <summary>
        /// Checks if date is => current date
        /// </summary>
            public CurrentDateAttribute()
            {
            }

            public override bool IsValid(object value)
            {
                var dt = (DateTime)value;
                if (dt >= DateTime.Now)
                {
                    return true;
                }
                return false;
            }
        }
    }

