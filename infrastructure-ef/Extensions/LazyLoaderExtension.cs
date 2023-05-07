using System.Runtime.CompilerServices;

namespace infrastructure_ef.Extensions;

public static class LazyLoaderExtension
{
    public static TRelated Load<TRelated>(
        this Action<object, string> loader,
        object entity,
        ref TRelated navigationField,
        [CallerMemberName] string navigationName = null)
        where TRelated : class
    {
        loader?.Invoke(entity, navigationName);
        return navigationField;
    }
}