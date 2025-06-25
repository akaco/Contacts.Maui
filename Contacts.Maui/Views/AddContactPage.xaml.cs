using Microsoft.Maui.Controls;
using System;

namespace Contacts.Maui.Views;

public partial class AddContactPage : ContentPage
{
	public AddContactPage()
	{
		InitializeComponent();
	}

	private async void OnChangePhotoClicked(object sender, EventArgs e)
	{
		try
		{
			// Implement photo picker functionality
			var result = await MediaPicker.PickPhotoAsync();
			if (result != null)
			{
				avatarImage.Source = ImageSource.FromFile(result.FullPath);
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Photo Selection", "Unable to select photo at this time.", "OK");
		}
	}

	private async void OnSaveClicked(object sender, EventArgs e)
	{
		// Validate required fields
		if (string.IsNullOrWhiteSpace(nameEntry.Text))
		{
			await DisplayAlert("Validation Error", "Please enter a name.", "OK");
			return;
		}

		if (string.IsNullOrWhiteSpace(phoneEntry.Text))
		{
			await DisplayAlert("Validation Error", "Please enter a phone number.", "OK");
			return;
		}

		try
		{
			// Create new contact object
			var newContact = new Contact
			{
				Name = nameEntry.Text.Trim(),
				PhoneNumber = phoneEntry.Text.Trim(),
				Email = emailEntry.Text?.Trim() ?? "",
				IsFavorite = favoriteSwitch.IsToggled,
				IsOnline = false,
				Avatar = "default_avatar.png" // You would set this to the selected image
			};

			// In a real app, you would save this to your data source
			// For now, we'll just show a success message
			await DisplayAlert("Success", $"Contact '{newContact.Name}' has been added successfully!", "OK");

			// Navigate back to contacts page
			await Shell.Current.GoToAsync("..");
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", "Failed to save contact. Please try again.", "OK");
		}
	}

	private async void OnCancelClicked(object sender, EventArgs e)
	{
		// Check if user has entered any data
		bool hasData = !string.IsNullOrWhiteSpace(nameEntry.Text) ||
					  !string.IsNullOrWhiteSpace(phoneEntry.Text) ||
					  !string.IsNullOrWhiteSpace(emailEntry.Text);

		if (hasData)
		{
			bool shouldDiscard = await DisplayAlert("Discard Changes",
				"Are you sure you want to discard your changes?",
				"Discard", "Continue Editing");

			if (!shouldDiscard)
				return;
		}

		// Navigate back
		await Shell.Current.GoToAsync("..");
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		// Focus on name field when page appears
		nameEntry.Focus();
	}
}
