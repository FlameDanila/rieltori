using System;
using Telegram.Bot.Types.ReplyMarkups;

namespace Rieltori
{
    internal class inlButtons
    {
        public static InlineKeyboardMarkup mainFunc = new(new[]
        {
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Авторизоваться", callbackData: "inlAuth"),
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Посмотреть профиль риелтора", callbackData: "inlProfile"),
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Посмотреть профиль риелтора (vol.2)", callbackData: "inlProfile2"),
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Выдать ЦРУ своё местоположение", callbackData: "inlMap"),
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Выбрать квартиры по фильтру", callbackData: "inlSearch"),
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Связаться с поддержкой", callbackData: "inlSupport"),
        }
    });
        public static InlineKeyboardMarkup removeAccount = new(new[]
        {
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Да", callbackData: "acceptRemoveAccount"),
            InlineKeyboardButton.WithCallbackData(text: "Нет", callbackData: "declineRemoveAccount"),
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Назад ↑", callbackData: "inlBack"),
        },
    });
    //    public static InlineKeyboardMarkup headerAnsverInl = new(new[]
    //    {
    //    new []
    //    {
    //        InlineKeyboardButton.WithCallbackData(text: "Пример", callbackData: "headerReferenceInl"),
    //    },
    //});
        public static InlineKeyboardMarkup supportRedirect = new(new[]
        {
        new []
        {
            InlineKeyboardButton.WithUrl(text: "Обратиться в поддержку🔗",url: "https://t.me/Flame_chanel"),
        },new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Назад ↑", callbackData: "inlBack"),
        },
    });
        public static InlineKeyboardMarkup backToMain = new(new[]
        {
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Назад ↑", callbackData: "inlBack"),
        },
    });
        public static ReplyKeyboardRemove hide = new ReplyKeyboardRemove();
    }
}
