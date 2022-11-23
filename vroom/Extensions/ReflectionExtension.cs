namespace vroom.Extensions
{
    public static class ReflectionExtension
    {
        public static string GetPropertyValue<T>(this T Item, string propertyName)
        {
            return Item.GetType().GetProperty(propertyName).GetValue(Item, null).ToString();
            //return item.GetType().GetProperty(propertyName).GetValue(item, null).ToString();

        }
    }
}
