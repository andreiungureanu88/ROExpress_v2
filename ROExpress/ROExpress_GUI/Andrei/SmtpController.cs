using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Activities.Statements;
using System.Windows;
using System.IO;
using System.Windows.Documents;
using System.Xml.Linq;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using PdfSharp;
using ZXing;
using ZXing.QrCode;
using System.Drawing;
using System.Drawing.Imaging;






namespace ROExpress_GUI.Andrei
{
    internal class SmtpController
    {

        public void SendMessage(List<Tickets> tickets)
        {

            string TicketPDFPath = TicketPDFGenerator(tickets);

            string senderEmail = "roexpressemail@gmail.com";
            string senderPassword = "umktevwyljgaqwpb"; // Use an application-specific password if you have 2-step verification enabled

            string recipientEmail = tickets[0].Email;

          

            try
            {
                MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail);
                mailMessage.Subject = "Bilet " + tickets[0].ID_Bilet + " " + tickets[0].Oras_Plecare + " => "
                    + tickets[0].Oras_Sosire + " " + tickets[0].Data;
                mailMessage.Body = "Ticket";

                Attachment attachment = new Attachment(TicketPDFPath);
                mailMessage.Attachments.Add(attachment);

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true,
                };
                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }

        }


        private System.Drawing.Bitmap GenerateQRCode(string text, int width, int height)
        {
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            barcodeWriter.Options = new ZXing.Common.EncodingOptions
            {
                Width = width,
                Height = height
            };

            return barcodeWriter.Write(text);
        }
        public string TicketPDFGenerator(List<Tickets> tickets)
        {
            
            string filePath = $".\\bilet_{tickets[0].ID_Bilet}.pdf";
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            // Add a page to the document
            foreach( var ticket in tickets)
            {
                PdfPage page = document.AddPage();

                page.Size = PageSize.A4;
                page.Orientation = PageOrientation.Portrait;

                XGraphics gfx = XGraphics.FromPdfPage(page);

                XFont font = new XFont("Verdana", 12, XFontStyle.Regular);
                XBrush brush = XBrushes.Black;

                double rectangleWidth = page.Width.Point * 0.6;
                double rectangleHeight = page.Height.Point * 0.6;
                double rectangleX = (page.Width.Point - rectangleWidth) / 2;
                double rectangleY = (page.Height.Point - rectangleHeight) / 2;

                gfx.DrawRectangle(XPens.Black, rectangleX, rectangleY, rectangleWidth, rectangleHeight);

                XRect layoutRectangle = new XRect(rectangleX, rectangleY, rectangleWidth, rectangleHeight);
                XStringFormat format = new XStringFormat
                {
                    Alignment = XStringAlignment.Center,
                    LineAlignment = XLineAlignment.Center
                };

                XFont titleFont = new XFont("Arial-BoldMT", 24, XFontStyle.Bold);
                XBrush titleBrush = XBrushes.IndianRed;

                layoutRectangle.Y -= 230;
                gfx.DrawString("ROExpress", titleFont, titleBrush, layoutRectangle, format);

                layoutRectangle.Y += 100;
                layoutRectangle.Y += 10;
                gfx.DrawString($"ID Bilet: {ticket.ID_Bilet}", font, brush, layoutRectangle, format);

                layoutRectangle.Y += 20;
                gfx.DrawString($"ID Tren: {ticket.ID_Tren}", font, brush, layoutRectangle, format);

                layoutRectangle.Y += 20;
                gfx.DrawString($"Oras Plecare: {ticket.Oras_Plecare}", font, brush, layoutRectangle, format);

                layoutRectangle.Y += 20;
                gfx.DrawString($"Oras Sosire: {ticket.Oras_Sosire}", font, brush, layoutRectangle, format);

                layoutRectangle.Y += 20;
                gfx.DrawString($"Data: {ticket.Data.ToLongDateString()}", font, brush, layoutRectangle, format);

                layoutRectangle.Y += 20;
                gfx.DrawString($"Clasa: {ticket.Clasa}", font, brush, layoutRectangle, format);

                layoutRectangle.Y += 20;
                gfx.DrawString($"Numar vagon: {ticket.NumarVagon}", font, brush, layoutRectangle, format);

                layoutRectangle.Y += 20;
                gfx.DrawString($"Loc Vagon: {ticket.LocVagon}", font, brush, layoutRectangle, format);

                layoutRectangle.Y += 20;

                string textToEncode = "https://www.cfrcalatori.ro";
                Bitmap qrCodeBitmap = GenerateQRCode(textToEncode, 300, 300);
                using (MemoryStream stream = new MemoryStream())
                {
                    qrCodeBitmap.Save(stream, ImageFormat.Png);

                    XImage image = XImage.FromStream(stream);

                    gfx.DrawImage(image, layoutRectangle.X + 130, layoutRectangle.Y + 250, 100, 100);
                }
            }
            document.Save(filePath);



            return filePath ;
        }
    }
}
