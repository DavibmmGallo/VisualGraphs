using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.UI.Popups;

namespace VisualGraphs.Classes
{
    class LogManager
    {
        private FileSavePicker savePicker;
        private MessageDialog msgdi;

        public LogManager()
        {
        }

        public async Task SaveAsync(string content)
        {
            savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
            savePicker.SuggestedFileName = "New Document";

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);
                await FileIO.WriteTextAsync(file, content);

                FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);

                if (status == FileUpdateStatus.Complete)
                {
                    msgdi = new MessageDialog($"File " + file.Name + " was saved.");
                }
                else
                {
                    msgdi = new MessageDialog($"File " + file.Name + " couldn't be saved.");
                }
            }
            else
            {
                msgdi = new MessageDialog($"Operation cancelled.");

            }
            await msgdi.ShowAsync();
        }

    }
}
