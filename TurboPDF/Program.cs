using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(options =>
{
    options.AllowAnyOrigin();

});
app.MapPost("/generate", ([FromBody] TurboPDF.PDF pdf) =>
{
    var doc = Document.Create(container =>
    {
        container.Page(page =>
        {
            foreach (var pdfPage in pdf.Pages)
            {
                page.Size(PageSizes.Letter);
            
                page.PageColor(Colors.White);
                //adjust page margin
                page.Margin(1,Unit.Inch);
                
                //default fontsize
                page.DefaultTextStyle(x => x.FontSize(14));
                page.Header().BorderBottom(1).Column((c) =>
                {
                  
                  
                    foreach (var header in pdf.Headers)
                    {
                        //header fontsize changed here
                        c.Item().Text(header.LineItemDescription).FontSize(24);
                    }
                });







                page.Content()
                    .Column(x =>
                    {
                        x.Item().Text(pdfPage.LineItemDescription);
                        x.Item().PageBreak();
                    });
       
            
            

            };
           
        }).Page(
            (desc) =>
            {
                desc.Margin(1,Unit.Inch);
                foreach (var pdfAttachment in pdf.Attachments)
                {

                    desc.Content().Column(c =>
                    {
                        c.Item().ScaleToFit().Image(Convert.FromBase64String(pdfAttachment.Content),ImageScaling.FitArea);
                       
                    });
                
            }

            });
    }).GeneratePdf();

    return Results.File(doc, contentType: "application/pdf");
});



app.Run();