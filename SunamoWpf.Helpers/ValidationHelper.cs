namespace SunamoWpf;

public class ValidationHelper
{
    public static bool checkForFalseValidated = false;

    static bool _validated = false;

    public static bool validated
    {
        set
        {
            if (checkForFalseValidated && !value)
            {

            }
            _validated = value;
        }
        get
        {
            return _validated;
        }
    }
}