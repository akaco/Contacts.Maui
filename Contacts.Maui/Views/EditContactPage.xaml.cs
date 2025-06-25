using Microsoft.Maui.Controls;
using System;

namespace Contacts.Maui.Views;

public partial class EditContactPage : ContentPage
{
	private Contact _originalContact;
	private Contact _currentContact;

	public EditContactPage()
	{
		InitializeComponent();
	}

	public EditContactPage(Contact contact) : this()
	{
		_originalContact = contact;
		_currentContact = new Contact
		{
			Name = contact.Name,
			PhoneNumber = contact.PhoneNumber,
			Email = contact.Email,
			Avatar = contact.Avatar,
			IsFavorite = contact.IsFavorite,
			IsOnline = contact.IsOnline
		};

		BindingContext = _currentContact;
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
				_currentContact.Avatar = result.FullPath;
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
			// Update contact object
			_currentContact.Name = nameEntry.Text.Trim();
			_currentContact.PhoneNumber = phoneEntry.Text.Trim();
			_currentContact.Email = emailEntry.Text?.Trim() ?? "";
			_currentContact.IsFavorite = favoriteSwitch.IsToggled;
			_currentContact.IsOnline = onlineSwitch.IsToggled;

			// In a real app, you would update this in your data source
			// For now, we'll just show a success message
			await DisplayAlert("Success", $"Contact '{_currentContact.Name}' has been updated successfully!", "OK");

			// Navigate back to contacts page
			await Shell.Current.GoToAsync("..");
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", "Failed to update contact. Please try again.", "OK");
		}
	}

	private async void OnDeleteClicked(object sender, EventArgs e)
	{
		bool shouldDelete = await DisplayAlert("Delete Contact",
			$"Are you sure you want to delete '{_currentContact.Name}'? This action cannot be undone.",
			"Delete", "Cancel");

		if (shouldDelete)
		{
			try
			{
				// In a real app, you would delete this from your data source
				await DisplayAlert("Success", $"Contact '{_currentContact.Name}' has been deleted.", "OK");

				// Navigate back to contacts page
				await Shell.Current.GoToAsync("..");
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error", "Failed to delete contact. Please try again.", "OK");
			}
		}
	}

	private async void OnCancelClicked(object sender, EventArgs e)
	{
		// Check if user has made any changes
		bool hasChanges = _currentContact.Name != _originalContact.Name ||
						 _currentContact.PhoneNumber != _originalContact.PhoneNumber ||
						 _currentContact.Email != _originalContact.Email ||
						 _currentContact.IsFavorite != _originalContact.IsFavorite ||
						 _currentContact.IsOnline != _originalContact.IsOnline;

		if (hasChanges)
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

		// Update the form fields with current contact data
		if (_currentContact != null)
		{
			nameEntry.Text = _currentContact.Name;
			phoneEntry.Text = _currentContact.PhoneNumber;
			emailEntry.Text = _currentContact.Email;
			favoriteSwitch.IsToggled = _currentContact.IsFavorite;
			onlineSwitch.IsToggled = _currentContact.IsOnline;
		}
	}
}
