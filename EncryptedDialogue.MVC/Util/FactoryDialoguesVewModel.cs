using System;
using System.Linq;
using System.Linq.Expressions;
using EncryptedDialogue.MVC.Models;

namespace EncryptedDialogue.MVC.Util
{
    public static class FactoryDialoguesVewModel
    {
        public static DialoguesVewModel FactoryDialogVewModel()
        {
            var applicationDbContext = new ApplicationDbContext();
            var modelContext = new EncryptedDialogueContext();
            return new DialoguesVewModel
            {
                ApplicationUsers = applicationDbContext.Users,
                Dialogues = modelContext.Dialogues.OrderByDescending(x => x.DateTime),
                FileDatas = modelContext.FileDatas
            };
        }

        public static DialoguesVewModel FactoryDialog(string recepientId, string senderId)
        {
            var applicationDbContext = new ApplicationDbContext();
            
            var dialoguesViewModel = new DialoguesVewModel
            {
                ApplicationUsers = applicationDbContext.Users
            };

            using (var modelContext = new EncryptedDialogueContext())
            {
                Expression<Func<Dialogue, bool>> dialogueSearchCriteria = x =>
                    (x.RecipientId == senderId && x.SenderId == recepientId) ||
                    (x.RecipientId == recepientId && x.SenderId == senderId);

                dialoguesViewModel.FileDatas = modelContext.FileDatas.ToList();
                var listMessages = modelContext.Dialogues.Where(dialogueSearchCriteria).ToList();

                dialoguesViewModel.Dialogues = listMessages;
            }

            return dialoguesViewModel;
        }
    }
}