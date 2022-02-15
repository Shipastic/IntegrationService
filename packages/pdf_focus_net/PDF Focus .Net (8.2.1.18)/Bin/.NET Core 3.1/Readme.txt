In case of you have added the reference to the SautinSoft.PdfFocus.dll by the old way (Solution Explorer->right click by "References"->Add Reference...), please add these (or newer versions) dependencies using Nuget:

    System.Drawing.Common, 4.7.0 or up.
    System.IO.Packaging, 4.4.0 or up.
    System.Text.Encoding.CodePages, 4.5.0 or up.
    System.Xml.XPath.XmlDocument, 4.3.0 or up.

For example, to add "System.IO.Packaging, 4.4.0:
(Solution Explorer->right click by "References"->Manage Nuget Packages...->In the tab "Browse" type "System.IO.Packaging" and find the "System.IO.Packaging", select version "4.4.0").

Detailed steps:
https://sautinsoft.net/help/pdf-to-word-tiff-images-text-rtf-csharp-vb-net/html/getting-started.htm

If you will have any questions, please don't hesitate to email us at: support@sautinsoft.com!