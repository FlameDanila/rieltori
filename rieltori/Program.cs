using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Data;
using System.IO;
using System;
using System.Threading;
using Telegram.Bot.Types.ReplyMarkups;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Formats.Asn1;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Net;
using System.Runtime.CompilerServices;
using Telegram.Bot.Types.InlineQueryResults;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Xml.Linq;
using System.Diagnostics;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using Telegram.Bot.Requests;
using System.Timers;
using System.Security;
using System.Runtime.InteropServices.ObjectiveC;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.CodeAnalysis;
using System.IO.Pipes;
using Rieltori;

internal class Program
{
    public static async Task Main()
    {
        var botClient = new TelegramBotClient("");

        using var cts = new CancellationTokenSource();

        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };
        botClient.StartReceiving(
            updateHandler: HandleUpdatesAsync,
            pollingErrorHandler: HandleErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: cts.Token
        );
        Console.ReadLine();

        var me = await botClient.GetMeAsync();

        static bool Block()
        {
            return true;
        }
        async Task HandleUpdatesAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                if (update.Message.Text == "/start")
                {
                    await Message(botClient, update.Message);
                }
                else
                {
                    await HandleMessage(botClient, update.Message);
                    return;
                }
            }
            if (update.Type == UpdateType.CallbackQuery)
            {
                await HandleCallbackQuery(botClient, update.CallbackQuery);
                return;
            }
            if (update.Message != null)
            {
                if (update.Message.Type == MessageType.Contact)
                {
                    await GetContactAsync(botClient, MessageType.Contact, update.Message);
                    return;
                }

                if (update.Message.Type == MessageType.Photo)
                {
                    await PhotoAnsver(botClient, MessageType.Photo, update.Message);
                    return;
                }
                if (update.Message.Type == MessageType.Document)
                {
                    await HandlePhoto(botClient, MessageType.Document, update.Message);
                    return;
                }
            }
        }
    }

    private static string Rearm(string name)
    {
        name = name.Replace("-", "\\-").Replace("+", "\\+").Replace(".", "\\.").Replace("(", "\\(").Replace(")", "\\)").Replace("/", "\\.").Replace("_", "\\_");
        return name;
    }
    private static async Task Message(ITelegramBotClient botClient, Message message)
    {
        string username = message.Chat.Username;
        string name = message.Chat.FirstName + " " + message.Chat.LastName;
        string telegramId = message.Chat.Id.ToString();
        string telegramId2 = message.From.Id.ToString();
        string chatId = message.Chat.Id.ToString();
        string ss = message.Text;

        int tMorning = 06;
        int tDay = 12;
        int tEvening = 18;
        int tNight = 00;
        var hourNow = DateTime.Now.Hour;

        try
        {
            if (hourNow >= tMorning && hourNow < tDay)
            {
                await botClient.SendTextMessageAsync(chatId, "Доброго утра!", replyMarkup: inlButtons.mainFunc);
            }
            else if (hourNow >= tDay && hourNow < tEvening)
            {
                await botClient.SendTextMessageAsync(chatId, "Доброго дня!", replyMarkup: inlButtons.mainFunc);
            }
            else if (hourNow >= tEvening && hourNow < 24)
            {
                await botClient.SendTextMessageAsync(chatId, "Доброго вечера!", replyMarkup: inlButtons.mainFunc);
            }
            else if (hourNow >= tNight && hourNow < tMorning)
            {
                await botClient.SendTextMessageAsync(chatId, "Доброй ночи!", replyMarkup: inlButtons.mainFunc);
            }
            else
            {
                await botClient.SendTextMessageAsync(chatId, "Нихуя не подходит, переделывай " + DateTime.Now.Hour.ToString());
            }    
        }
        catch (Exception ex)
        {
            FileStream notificationsStream = new FileStream("ErrorsLogsRieltori.txt", FileMode.Append); // Логи

            string sMessage = ex.ToString() + "    " + DateTime.Now + "\n----------------------------------------------------------\n";
            StreamWriter streamWriter = new StreamWriter(notificationsStream);

            streamWriter.WriteLine(sMessage + "\n");

            streamWriter.Close();
            notificationsStream.Close();
            Console.WriteLine("ErrorsLogs");
        }
    }

    private static async Task HandleMessage(ITelegramBotClient botClient, Message message)
    {
        string username = message.Chat.Username;
        string name = message.Chat.FirstName + " " + message.Chat.LastName;
        string telegramId = message.Chat.Id.ToString();
        string telegramId2 = message.From.Id.ToString();
        string chatId = message.Chat.Id.ToString();
        string ss = message.Text;

        try
        {
            if (ss != "")
            {

            }
            FileStream notificationsStream = new FileStream("telegramBotmessagesProfit.txt", FileMode.Append); // Логи

            string sMessage = "message = " + ss + "\t" + DateTime.Now + "\t" + chatId + "\t" + username;
            StreamWriter streamWriter = new StreamWriter(notificationsStream);

            streamWriter.WriteLine(sMessage);

            streamWriter.Close();
            notificationsStream.Close();
        }
        catch (Exception ex)
        {
            FileStream notificationsStream = new FileStream("ErrorsLogsProfit.txt", FileMode.Append); // Логи

            string sMessage = ex.ToString() + "    " + DateTime.Now;
            StreamWriter streamWriter = new StreamWriter(notificationsStream);

            streamWriter.WriteLine(sMessage + "\n");

            streamWriter.Close();
            notificationsStream.Close();
            Console.WriteLine("ErrorsLogs");
        }
    }
    private static async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
    {
        string username = callbackQuery.Message.Chat.Username;
        string name = callbackQuery.Message.Chat.FirstName + " " + callbackQuery.Message.Chat.LastName;
        string telegramId = callbackQuery.Message.Chat.Id.ToString();
        string telegramId2 = callbackQuery.Message.From.Id.ToString();
        string ss = callbackQuery.Message.Text;

        string chatId = callbackQuery.Message.Chat.Id.ToString();
        var messageId = callbackQuery.Message.MessageId;
        try
        {
            switch (callbackQuery.Data)
            {
                case "inlSupport":
                    await botClient.EditMessageTextAsync(chatId, messageId, "Если у вас возникли вопросы:", replyMarkup: inlButtons.supportRedirect);
                    break;
                case "inlRemoveAccount":
                    await botClient.EditMessageTextAsync(chatId, messageId, "Вы хотите *полностью* удалить свой аккаунт\\?", replyMarkup: inlButtons.removeAccount, parseMode: ParseMode.MarkdownV2);
                    break;
                case "acceptRemoveAccount":
                    await botClient.EditMessageTextAsync(chatId, messageId, "Ваш аккаунт успешно удалён!", replyMarkup: inlButtons.mainFunc);
                    break;
                case "inlBack":
                    await botClient.EditMessageTextAsync(chatId, messageId, "Выберите функцию: ", replyMarkup: inlButtons.mainFunc);
                    break;
                case "inlProfile":
                    FileStream file = new FileStream("C:\\Users\\Danila\\Pictures\\186212515.jpg", FileMode.Open);
                    await botClient.SendPhotoAsync(chatId, InputFile.FromStream(file));
                    await botClient.SendTextMessageAsync(chatId, "Имя " + Rearm(name) + "\n\n" + "Аккаунт t\\.me/" + Rearm(username) + "\n\nНомер телефона `89504951460`", replyMarkup: inlButtons.backToMain, parseMode: ParseMode.MarkdownV2);
                    break;
                case "inlProfile2":
                    await botClient.EditMessageTextAsync(chatId, messageId, "Имя: " + Rearm(name) + "\n\n" + "Аккаунт: `"+username+"`\n\nНомер телефона: `89504951460` [ ](http://putin.kremlin.ru/img/putin__bg1@2x-2600.jpg)", replyMarkup: inlButtons.backToMain, parseMode: ParseMode.MarkdownV2);
                    break;
                case "inlSearch":
                    await botClient.SendTextMessageAsync(chatId, "Нет бд - нет квартир, ничо не знаю", replyMarkup: inlButtons.backToMain);
                    break;
                case "inlAuth":
                    await botClient.SendTextMessageAsync(chatId, "Нажмите на кнопку для отправки паспорта Telegram", replyMarkup: replyButtons.shareContact);
                    break;
                case "inlMap":
                    await botClient.SendTextMessageAsync(chatId, "Нажмите на кнопку для отправки местоположения", replyMarkup: replyButtons.shareContact2);
                    break;
            }
            FileStream notificationsStream = new FileStream("telegramBotСallbackQueryProfit.txt", FileMode.Append); // Логи

            string sMessage = "callbackQuery = " + callbackQuery + "\t" + DateTime.Now + "\t" + chatId + "\t" + username;
            StreamWriter streamWriter = new StreamWriter(notificationsStream);

            streamWriter.WriteLine(sMessage);

            streamWriter.Close();
            notificationsStream.Close();
        }
        catch (Exception ex)
        {
            FileStream notificationsStream = new FileStream("ErrorsLogsProfit.txt", FileMode.Append); // Логи

            string sMessage = ex.ToString() + "    " + DateTime.Now;
            StreamWriter streamWriter = new StreamWriter(notificationsStream);

            streamWriter.WriteLine(sMessage);
            Console.WriteLine(sMessage);
            streamWriter.Close();
            notificationsStream.Close();
            Console.WriteLine("ErrorsLogs");
        }
    }
    private static async Task GetContactAsync(ITelegramBotClient botClient, MessageType messageType, Message message)
    {
        //await botClient.SendTextMessageAsync(message.Chat.Id, $"You said:\n{message.Contact.PhoneNumber}");
        var username = message.Chat.Username;
        var name = message.Chat.FirstName + " " + message.Chat.LastName;
        var chatId = message.Chat.Id;
        var ss = message.Text;
        var telegramId = message.From.Id;
        var phoneNumber = message.Contact.PhoneNumber.Replace("+", "").Remove(0, 1);

        try
        {
            await botClient.SendTextMessageAsync(chatId, $"Номер \\- _*{phoneNumber}*_", replyMarkup: inlButtons.hide, parseMode: ParseMode.MarkdownV2);
        }
        catch (Exception ex)
        {
            FileStream notificationsStream = new FileStream("ErrorsLogsProfit.txt", FileMode.Append); // Логи

            string sMessage = ex.ToString() + "    " + DateTime.Now;
            StreamWriter streamWriter = new StreamWriter(notificationsStream);

            streamWriter.WriteLine(sMessage);

            streamWriter.Close();
            notificationsStream.Close();
            Console.WriteLine("ErrorsLogs");
        }
    }
    private static async Task PhotoAnsver(ITelegramBotClient botClient, MessageType messageType, Message message)
    {
        var username = message.Chat.Username;
        var name = message.Chat.FirstName + " " + message.Chat.LastName;
        var chatId = message.Chat.Id;
        var ss = message.Photo;
        var telegramId = message.From.Id;

        DataTable photo = await Select($"select addIcon, editIcon, choose from usersGorbTelegram where telegramId = '{chatId}'");
        if (photo.Rows.Count != 0)
        {
            await botClient.SendTextMessageAsync(chatId, "Отправьте фото документом, пожалуйста");
        }
    }
    private static async Task HandlePhoto(ITelegramBotClient botClient, MessageType messageType, Message message)
    {
        try
        {
            var username = message.Chat.Username;
            var name = message.Chat.FirstName + " " + message.Chat.LastName;
            var chatId = message.Chat.Id;
            var ss = message.Photo;
            var telegramId = message.From.Id;

            var fileId = message.Document.FileId;
            var fileInfo = await botClient.GetFileAsync(fileId);
            var filePath = fileInfo.FilePath;

            DataTable photo = await Select($"select addIcon, editIcon, choose from usersGorbTelegram where telegramId = '{chatId}'");

            string destinationFilePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{message.Document.FileName}";

            await using Stream fileStream = System.IO.File.Create(destinationFilePath);
            await botClient.DownloadFileAsync(
                filePath: filePath,
                destination: fileStream);

            await botClient.SendTextMessageAsync(chatId, "Классное фото");
            if (photo.Rows.Count != 0)
            {
                if (photo.Rows[0][0].ToString() == "True")
                {
                    await Select($"update applicationsData set imageRoute = '{filePath}' where id = '{photo.Rows[0][2]}'");
                }
                else if (photo.Rows[0][1].ToString() == "True")
                {
                    await Select($"update applicationsData set imageRoute = '{filePath}' where id = '{photo.Rows[0][2]}'");
                }
            }
        }
        catch (Exception ex)
        {
            FileStream notificationsStream = new FileStream("ErrorsLogsProfit.txt", FileMode.Append); // Логи

            string sMessage = ex.ToString() + "    " + DateTime.Now;
            StreamWriter streamWriter = new StreamWriter(notificationsStream);

            streamWriter.WriteLine(sMessage);

            streamWriter.Close();
            notificationsStream.Close();
            Console.WriteLine("ErrorsLogs");
        }
    }
    private static Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Ошибка телеграм АПИ:\n{apiRequestException.ErrorCode}\n{apiRequestException.Message}",
            _ => exception.ToString()
        };
        FileStream notificationsStream = new FileStream("ErrorsLogsProfit.txt", FileMode.Append); // Логи

        string sMessage = ErrorMessage.ToString() + "\n   " + DateTime.Now;
        StreamWriter streamWriter = new StreamWriter(notificationsStream);

        streamWriter.WriteLine(sMessage);

        streamWriter.Close();
        notificationsStream.Close();
        Console.WriteLine("ErrorsLogs");
        return Task.CompletedTask;
    }
    private static async Task<DataTable> Select(string selectSQL)
    {
        DataTable data = new DataTable("dataBase");

        //SqlConnection sqlConnection = new SqlConnection($"server=DESKTOP-ITVEB8Q\\sqlexpress;Trusted_connection=yes;DataBase=GorbJobData;User=;PWD=");
        SqlConnection sqlConnection = new SqlConnection($"server=serv-1c;Trusted_connection=no;DataBase=grobTelegram;User=sa;PWD=DkW52W9dz23Delfa");
        sqlConnection.Open();

        SqlCommand sqlCommand = sqlConnection.CreateCommand();
        sqlCommand.CommandText = selectSQL;

        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
        sqlDataAdapter.Fill(data);

        sqlCommand.Dispose();
        sqlDataAdapter.Dispose();
        sqlConnection.Close();

        return data;
    }//Запрос
}
