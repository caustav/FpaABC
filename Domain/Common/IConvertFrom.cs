namespace Domain.Common
{
    interface IConvertFrom<T>
    {
         T ConvertFrom(string str);
    }
}