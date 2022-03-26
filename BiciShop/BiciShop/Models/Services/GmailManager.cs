using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using BLL.DTO;

namespace BiciShop.Models
{
    public class GmailManager
    {
        private static string GetMessageText(OrderDTO order)
        {
            string messageText =
             @$"<HTML>
                <head>
            <link rel='stylesheet' href='https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/css/bootstrap.min.css' integrity='sha384-zCbKRCUGaJDkqS1kPbPd7TveP5iyJE0EjAuZQTgFLD2ylzuqKfdKlfG/eSrtxUkn' crossorigin='anonymous'>
            </head>
            <BODY style='margin: 3rem;'>
             <span style='font-family: Verdana; font-size: 9pt'>
             Dear {order.Name} {order.Lastname},<br/><br/>
             At {order.Date}<br/>
             You ordered these goods for <b>{order.TotalValue} ₴</b>:<br/><br/>
            </span><br />
            <div style='display: flex; flex-wrap: wrap; justify-content: space-around;'>";
            foreach (var unit in order.OrderUnits)
            {
                string imgSrc = "src=\"data:image/png;base64," + unit.Bicicleta.Photo + "\"";
                messageText += @$"
            <div class='card mb-3 item' style='width: 18rem; '>
                 <img {imgSrc}
                     class='card-img-top'
                     style='max-height: 18rem; max-width: 18rem;'
                     alt='bici photo'>
                <div class='card-body'>
                    <h5 class='card-title'>{unit.Bicicleta.Title}</h5>
                    <h3 class='text-success mr-1'>{unit.GetValue()} ₴</h3>
                    <p class='card-text'>Count - {unit.Count}</p>
                </div>
            </div>";
            }

            messageText += @"
            <script src='https://cdn.jsdelivr.net/npm/jquery@3.5.1/dist/jquery.slim.min.js' integrity='sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj' crossorigin='anonymous'></script>
            <script src ='https://cdn.jsdelivr.net/npm/bootstrap@4.6.1/dist/js/bootstrap.bundle.min.js' integrity = 'sha384-fQybjgWLrvvRgtW6bFlB7jaZrFsaBXjsOMm/tB9LTS58ONXgqbR9W8oWht/amnpF' crossorigin='anonymous'></script >
     </div></BODY></HTML>";

            return messageText;
        }
        public static void Send(OrderDTO order)
        {
            string messageText = GetMessageText(order);
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("my.itstep.service@gmail.com");
            message.To.Add(new MailAddress(order.Email));
            message.Subject = "Info about ordered bicicleta";

            message.IsBodyHtml = true;
            message.Body = messageText;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("my.itstep.service@gmail.com", "84l56SCp");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            
            smtp.Send(message);
        }
    }
}
