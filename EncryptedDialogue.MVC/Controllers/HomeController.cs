using EncryptedDialogue.MVC.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using EncryptedDialogue.MVC.Hubs;
using EncryptedDialogue.MVC.Util;

namespace EncryptedDialogue.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index(string recepientId, string messageText)
        {
            return View(FactoryDialoguesVewModel.FactoryDialogVewModel());
        }

        public ActionResult Dialogue(string recepientId)
        {
            return View(FactoryDialoguesVewModel.FactoryDialog(recepientId, User.Identity.GetUserId()));
        }

        [HttpPost]
        public ActionResult Dialogue(string recepientId, string messageText, HttpPostedFileBase uploadFile = null)
        {
            SendMessage("Новое сообщение", recepientId);
            
            if (string.IsNullOrEmpty(messageText) && uploadFile == null)
            {
                return View(FactoryDialoguesVewModel.FactoryDialog(recepientId, User.Identity.GetUserId()));
            }

            var dialogue = new Dialogue
            {
                DateTime = DateTime.Now,
                SenderId = User.Identity.GetUserId(),
                RecipientId = recepientId
            };

            if (!string.IsNullOrEmpty(messageText))
            {
                dialogue.MessageText = messageText;
            }

            if (uploadFile != null)
            {
                var fileData = UnityProcessingCipher.StartEncodeProcess(uploadFile);
                dialogue.FileData = fileData;
            }

            using (var db = new EncryptedDialogueContext())
            {
                db.Dialogues.Add(dialogue);
                db.SaveChanges();
            }

            return View(FactoryDialoguesVewModel.FactoryDialog(recepientId, User.Identity.GetUserId()));
        }

        private static void SendMessage(string message, string recepientId)
        {
            var context =
                Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            var n = new NotificationHub();

            context.Clients.All.displayMessage(message);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Download(int? id)
        {
            FileData fileData;
            using (var db = new EncryptedDialogueContext())
            {
                if (id != null)
                {
                    fileData = db.FileDatas.FirstOrDefault(x => x.Id == id);

                    if (User.Identity.GetUserId() != db.Dialogues.First(x => x.FileDataId == id).RecipientId)
                    {
                        if (User.Identity.GetUserId() != db.Dialogues.First(x => x.FileDataId == id).SenderId)
                        {
                            return File(fileData.ByteArrayData, System.Net.Mime.MediaTypeNames.Application.Octet, fileData.Name);
                        }
                    }
                }
                else
                {
                    return HttpNotFound();
                }
            }

            var fileBytes = UnityProcessingCipher.StartDecodeProcess(fileData);
            var currentName = fileData.Name.LastIndexOf('.') > 0 ? fileData.Name.Substring(0, fileData.Name.LastIndexOf('.')) : fileData.Name;

            var fileName = string.Join(".", currentName, fileData.FileFormat.ToString());
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

            //Download EnCoDeD file ^_^
            //return File(fileData.ByteArrayData, System.Net.Mime.MediaTypeNames.Application.Octet, fileData.Name);
        }

        public ActionResult Play(int? id)
        {
            FileData fileData;
            using (var db = new EncryptedDialogueContext())
            {
                if (id != null)
                {
                    fileData = db.FileDatas.FirstOrDefault(x => x.Id == id);
                }
                else
                {
                    return HttpNotFound();
                }
            }

            return File(fileData.ByteArrayData, System.Net.Mime.MediaTypeNames.Application.Octet, fileData.Name);
        }
    }
}