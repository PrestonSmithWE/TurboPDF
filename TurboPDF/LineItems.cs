namespace TurboPDF;
// Root myDeserializedClass = JsonConvert.Deserializestring<Root>(myJsonResponse);
public class Attachment
{
    public string LineItemDescription { get; set; }
    public string Content { get; set; }
    public string InputType { get; set; }
}

public class PDF
{
    public string Title { get; set; }
    public List<Header> Headers { get; set; }
    public List<Page> Pages { get; set; }
    public List<Attachment> Attachments { get; set; }
}

public class Header
{
    public string LineItemDescription { get; set; }
    public string Content { get; set; }
    public string InputType { get; set; }
}

public class Page
{
    public string LineItemDescription { get; set; }
    public string Content { get; set; }
    public string InputType { get; set; }
}



