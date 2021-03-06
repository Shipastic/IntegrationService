Imports System.IO
Imports System.Drawing.Imaging
Imports System.Collections.Generic
Imports SautinSoft

Module Sample

    Sub Main()
        ' Extract all images from 1st PDF page
        Dim f As New SautinSoft.PdfFocus()

        ' This property is necessary only for registered version
        ' f.Serial = "XXXXXXXXXXX"
        Dim pdfFile As String = "..\simple text.pdf"
        Dim imageDir As String = (New DirectoryInfo(Directory.GetCurrentDirectory())).CreateSubdirectory("images").FullName

        Dim pdfImages As List(Of PdfFocus.PdfImage) = Nothing

        f.OpenPdf(pdfFile)

        If f.PageCount > 0 Then
            pdfImages = f.ExtractImages(1, 1)

            ' Show all extracted images.
            If pdfImages IsNot Nothing AndAlso pdfImages.Count > 0 Then

                For i As Integer = 0 To pdfImages.Count - 1
                    Dim imageFile As String = Path.Combine(imageDir, String.Format("img{0}.png", i + 1))
                    pdfImages(i).Picture.Save(imageFile)
                Next i
                System.Diagnostics.Process.Start(New System.Diagnostics.ProcessStartInfo(imageDir) With {.UseShellExecute = True})
            End If
        End If
    End Sub
End Module
