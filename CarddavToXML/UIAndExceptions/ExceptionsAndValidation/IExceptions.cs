namespace PhonebookConverter.UIAndExceptions.ExceptionsAndValidation
{
    public interface IExceptions
    {
        void ExceptionsLoop(Action metoda);
        T ExceptionsLoop<T>(Func<T> method);
    }
}
