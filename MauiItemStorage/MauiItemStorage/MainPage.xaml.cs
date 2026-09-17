using System.Collections.ObjectModel;
using MauiItemStorage.DataAccess;
using MauiItemStorage.Models;

namespace MauiItemStorage
{
    public partial class MainPage : ContentPage
    {
        ItemData itemData;

        public ObservableCollection<Item> Items { get; set; } = new();

        public MainPage()
        {
            InitializeComponent();

            itemData = new ItemData();

            BindingContext = this;

            UpdateItemList();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtItemID.Text) ||
                string.IsNullOrWhiteSpace(txtItemName.Text) ||
                string.IsNullOrWhiteSpace(txtItemDescription.Text))
            {
                await DisplayAlert("Error", "All fields are required.", "OK");
                return;
            }

            if (!int.TryParse(txtItemID.Text, out int itemID))
            {
                await DisplayAlert("Error", "Item ID must be a number.", "OK");
                return;
            }

            var item = new Item
            {
                ItemID = itemID,
                ItemName = txtItemName.Text,
                ItemDescription = txtItemDescription.Text
            };

            await itemData.SaveItemAsync(item);

            await UpdateItemList();

            txtItemID.Text = string.Empty;
            txtItemName.Text = string.Empty;
            txtItemDescription.Text = string.Empty;
        }

        private async Task UpdateItemList()
        {
            var items = await itemData.GetItemsAsync();

            Items.Clear();

            foreach (var item in items)
            {
                Items.Add(item);
            }
        }
    }
}