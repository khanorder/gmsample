using Microsoft.AspNetCore.Mvc.Formatters;

namespace NGEL.Data.HttpFormatter
{
    public class TextPlainInputFormatter : InputFormatter
    {
        private const string ContentsType = "text/plain";

        public TextPlainInputFormatter()
        {
            SupportedMediaTypes.Add(ContentsType);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            using (var reader = new StreamReader(request.Body))
            {
                var content = await reader.ReadToEndAsync();
                return await InputFormatterResult.SuccessAsync(content);
            }
        }

        public override bool CanRead(InputFormatterContext context)
        {
            var contentType = context.HttpContext.Request.ContentType;

            if (contentType == null)
                return false;

            return contentType.StartsWith(ContentsType);
        }

        protected override bool CanReadType(Type type)
        {
            //Put whatever types you want to handle. 
            return type == typeof(string) ||
                type == typeof(int) ||
                type == typeof(DateTime);
        }
    }
}