using System;
using Telegram.Bot.Types.ReplyMarkups;

namespace Rieltori
{
    internal class replyButtons
    {

        public static ReplyKeyboardMarkup shareContact = new(new[]
        {
            KeyboardButton.WithRequestContact("📞Отправить контакт📞")
        });
        public static ReplyKeyboardMarkup shareContact2 = new(new[] { KeyboardButton.WithRequestLocation("➡Отправить местоположение⬅") });
        public static ReplyKeyboardRemove hide = new ReplyKeyboardRemove();
    }
}
