using System;
using System.Diagnostics;
using System.IO;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

    
app.MapGet("/", async context =>
{

    context.Response.Headers.Add("Content-Type","text/html; charset=utf-8");
    await context.Response.WriteAsync($"<h1>Laguage accepted Jefferson's localhost</h1>");

    string test = context.Request.Headers["Accept-Language"];
    await context.Response.WriteAsync($"Hello World = {test}<br>");
    await context.Response.WriteAsync($"<ul>");
    string[] keyValue = test.Split(';');
    foreach(var item in keyValue)
    {
        await context.Response.WriteAsync($"<li>");
        string[] cv = item.Split(',');
        await context.Response.WriteAsync($"<b>Chave</b>: {cv[0]}<br>");
        await context.Response.WriteAsync($"<b>Valor</b>: {cv[1]}<br>");
        await context.Response.WriteAsync($"</li>");
    }
    await context.Response.WriteAsync($"</ul>");
});


app.MapGet("/csv", async context =>
{

    context.Response.Headers.Add("Content-Type","text/csv; charset=utf-8");
    await context.Response.WriteAsync($"Chave; Valor");
    string test = context.Request.Headers["Accept-Language"];
    string[] keyValue = test.Split(';');
    foreach(var item in keyValue)
    {
        string[]? cv = item.Split(',');
        await context.Response.WriteAsync($"{cv[0]};{cv[1]}");
    }
});


app.MapGet("/pdf", async context =>
{

    context.Response.Headers.Add("Content-Type","application/pdf; charset=utf-8");
    
     // Create a new PDF document
      PdfDocument document = new PdfDocument();
      document.Info.Title = "Created with PDFsharp";
 
      // Create an empty page
      PdfPage page = document.AddPage();
 
      // Get an XGraphics object for drawing
      XGraphics gfx = XGraphics.FromPdfPage(page);
 
      // Create a font
      XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);
 
      // Draw the text
      gfx.DrawString("Hello, Jefferson!", font, XBrushes.Black,new XRect(0, 0, page.Width, page.Height),XStringFormats.Center);
 
      // Save the document...
      const string filename = "HelloWorld.pdf";
      document.Save(filename);
      // ...and start a viewer.
      string doc = File.ReadAllText(filename);

      await context.Response.WriteAsync(doc);
      
    
});

app.MapGet("/query-string", async context =>
{
    context.Response.Headers.Add("Content-type", "text/html; charset=utf-8");
    await context.Response.WriteAsync($"<h1>Parâmetros no http</h1>");
    string name = context.Request.Query["jefferson"].ToString();
    string sirname = context.Request.Query["rocha"].ToString();
    await context.Response.WriteAsync($"<h1>Parâmetros no http</h1>");
    await context.Response.WriteAsync($"First Name = {name}<br>");
    await context.Response.WriteAsync($"Last Name = {sirname}<br>"); 
});


app.MapPost("/form-data", async context =>
{

    /*
    <!DOCTYPE html>
<html>
<body>

<h2>HTML Forms</h2>

<form method = "post" action="http://localhost:5001/form-data">
  <label for="fname">First name:</label><br>
  <input type="text" id="fname" name="jefferson" value=""><br>
  <label for="lname">Last name:</label><br>
  <input type="text" id="lname" name="rocha" value=""><br><br>
  <input type="submit" value="Submit">
</form> 

<p>If you click the "Submit" button, the form-data will be sent to a page called "/action_page.php".</p>

</body>
</html>


    */
    context.Response.Headers.Add("Content-type", "text/html; charset=utf-8");
    var dict = context.Request.Form.ToDictionary(x=> x.Key, x=> x.Value.ToString());
    string name = dict["jefferson"].ToString();
    string sirname = dict["rocha"].ToString();
    await context.Response.WriteAsync($"<h1>Parâmetros no http</h1>");
    await context.Response.WriteAsync($"First Name = {name}<br>");
    await context.Response.WriteAsync($"Last Name = {sirname}<br>"); 
});


app.Run();
