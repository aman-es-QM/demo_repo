using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using HtmlAgilityPack;  // Make sure you have installed this via NuGet

public class HtmlBeautifyMiddleware
{
    private readonly RequestDelegate _next;

    public HtmlBeautifyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.ContentType?.Contains("text/html") == true)
        {
            var originalBodyStream = context.Response.Body;
            using (var newBodyStream = new MemoryStream())
            {
                context.Response.Body = newBodyStream;

                await context.Response.WriteAsync(await GetFormattedHtml(context));

                newBodyStream.Seek(0, SeekOrigin.Begin);
                await newBodyStream.CopyToAsync(originalBodyStream);
            }
        }
    }

    private async Task<string> GetFormattedHtml(HttpContext context)
    {
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var reader = new StreamReader(context.Response.Body);
        var content = await reader.ReadToEndAsync();

        return BeautifyHtml(content);
    }

    private string BeautifyHtml(string html)
    {
        var doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(html);
        return doc.DocumentNode.OuterHtml;
    }
}
