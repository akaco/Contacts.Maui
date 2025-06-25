using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;

namespace Contacts.Maui.Views;

public partial class ContactsPage : ContentPage
{
	private double _lastScrollY = 0;
	private bool _isHeaderVisible = true;
	public ObservableCollection<Contact> Contacts { get; set; }

	public ContactsPage()
	{
		InitializeComponent();
		LoadDummyData();
		BindingContext = this;
		UpdateContactCount();
	}

	private void LoadDummyData()
	{
		Contacts = new ObservableCollection<Contact>
		{
			new Contact
			{
				Name = "Alice Johnson",
				PhoneNumber = "+1 (555) 123-4567",
				Email = "alice.johnson@email.com",
				Avatar = "https://randomuser.me/api/portraits/women/1.jpg",
				IsFavorite = true,
				IsOnline = true
			},
			new Contact
			{
				Name = "Bob Smith",
				PhoneNumber = "+1 (555) 234-5678",
				Email = "bob.smith@email.com",
				Avatar = "https://randomuser.me/api/portraits/men/1.jpg",
				IsFavorite = false,
				IsOnline = false
			},
			new Contact
			{
				Name = "Carol Davis",
				PhoneNumber = "+1 (555) 345-6789",
				Email = "carol.davis@email.com",
				Avatar = "https://randomuser.me/api/portraits/women/2.jpg",
				IsFavorite = true,
				IsOnline = true
			},
			new Contact
			{
				Name = "David Wilson",
				PhoneNumber = "+1 (555) 456-7890",
				Email = "david.wilson@email.com",
				Avatar = "https://randomuser.me/api/portraits/men/2.jpg",
				IsFavorite = false,
				IsOnline = true
			},
			new Contact
			{
				Name = "Emma Brown",
				PhoneNumber = "+1 (555) 567-8901",
				Email = "emma.brown@email.com",
				Avatar = "https://randomuser.me/api/portraits/women/3.jpg",
				IsFavorite = true,
				IsOnline = false
			},
			new Contact
			{
				Name = "Frank Miller",
				PhoneNumber = "+1 (555) 678-9012",
				Email = "frank.miller@email.com",
				Avatar = "https://randomuser.me/api/portraits/men/3.jpg",
				IsFavorite = false,
				IsOnline = true
			},
			new Contact
			{
				Name = "Grace Taylor",
				PhoneNumber = "+1 (555) 789-0123",
				Email = "grace.taylor@email.com",
				Avatar = "https://randomuser.me/api/portraits/women/4.jpg",
				IsFavorite = true,
				IsOnline = true
			},
			new Contact
			{
				Name = "Henry Anderson",
				PhoneNumber = "+1 (555) 890-1234",
				Email = "henry.anderson@email.com",
				Avatar = "https://randomuser.me/api/portraits/men/4.jpg",
				IsFavorite = false,
				IsOnline = false
			},
			new Contact
			{
				Name = "Ivy Martinez",
				PhoneNumber = "+1 (555) 901-2345",
				Email = "ivy.martinez@email.com",
				Avatar = "https://randomuser.me/api/portraits/women/5.jpg",
				IsFavorite = true,
				IsOnline = true
			},
			new Contact
			{
				Name = "Jack Thompson",
				PhoneNumber = "+1 (555) 012-3456",
				Email = "jack.thompson@email.com",
				Avatar = "https://randomuser.me/api/portraits/men/5.jpg",
				IsFavorite = false,
				IsOnline = true
			},
			new Contact
			{
				Name = "Kate White",
				PhoneNumber = "+1 (555) 123-4567",
				Email = "kate.white@email.com",
				Avatar = "https://randomuser.me/api/portraits/women/6.jpg",
				IsFavorite = true,
				IsOnline = false
			},
			new Contact
			{
				Name = "Leo Garcia",
				PhoneNumber = "+1 (555) 234-5678",
				Email = "leo.garcia@email.com",
				Avatar = "https://randomuser.me/api/portraits/men/6.jpg",
				IsFavorite = false,
				IsOnline = true
			}
		};
	}

	private void UpdateContactCount()
	{
		contactCountLabel.Text = $"{Contacts.Count} contacts";
	}

	// Handle scroll events for dynamic sizing
	private async void OnScrolled(object sender, ScrolledEventArgs e)
	{
		var scrollY = e.ScrollY;
		var scrollDelta = scrollY - _lastScrollY;

		// Animate header section based on scroll
		if (scrollY > 50 && _isHeaderVisible && scrollDelta > 0)
		{
			// Scroll down - hide/minimize header
			_isHeaderVisible = false;
			await Task.WhenAll(
				headerSection.FadeTo(0.3, 200, Easing.CubicOut),
				headerSection.ScaleTo(0.9, 200, Easing.CubicOut)
			);
		}
		else if (scrollY < 50 && !_isHeaderVisible)
		{
			// Scroll up - show header
			_isHeaderVisible = true;
			await Task.WhenAll(
				headerSection.FadeTo(1.0, 200, Easing.CubicOut),
				headerSection.ScaleTo(1.0, 200, Easing.CubicOut)
			);
		}

		// Animate contact items based on scroll position
		AnimateContactItems(scrollY);

		_lastScrollY = scrollY;
	}

	private void AnimateContactItems(double scrollY)
	{
		// Get visible contact items and animate them
		var visibleItems = GetVisibleContactItems();

		foreach (var item in visibleItems)
		{
			var itemPosition = GetItemPosition(item);
			var distanceFromCenter = Math.Abs(itemPosition - (DeviceDisplay.MainDisplayInfo.Height / 2));
			var normalizedDistance = Math.Min(distanceFromCenter / 200, 1.0);

			// Scale items based on distance from center
			var scale = 1.0 - (normalizedDistance * 0.05);
			var opacity = 1.0 - (normalizedDistance * 0.2);

			item.Scale = Math.Max(scale, 0.95);
			item.Opacity = Math.Max(opacity, 0.8);
		}
	}

	private List<View> GetVisibleContactItems()
	{
		// This is a simplified version - you might need to implement
		// proper visible item detection based on your specific needs
		return new List<View>();
	}

	private double GetItemPosition(View item)
	{
		// Calculate item position relative to screen
		// This would need proper implementation based on your layout
		return 0;
	}

	// Event handlers for buttons
	private async void btnAddContact_Clicked(object sender, EventArgs e)
	{
		try
		{
			var button = sender as Button;

			// Button animation
			await button.ScaleTo(0.9, 100);
			await button.ScaleTo(1.0, 100);

			// Navigate to add contact page
			await Shell.Current.GoToAsync("//addcontact");
		}
		catch (Exception ex)
		{
			// If Shell navigation fails, try alternative navigation
			try
			{
				await Navigation.PushAsync(new AddContactPage());
			}
			catch
			{
				// If no AddContactPage exists, show alert
				await DisplayAlert("Add Contact", "Add contact functionality will be implemented here.", "OK");
			}
		}
	}

	private async void btnEditContact_Clicked(object sender, EventArgs e)
	{
		try
		{
			var button = sender as Button;

			// Button animation
			await button.ScaleTo(0.9, 100);
			await button.ScaleTo(1.0, 100);

			// Get the contact from the button's binding context
			if (button.BindingContext is Contact selectedContact)
			{
				// Navigate to edit contact page with contact data
				await Shell.Current.GoToAsync($"//editcontact?contactId={selectedContact.Name}");
			}
		}
		catch (Exception ex)
		{
			// If Shell navigation fails, try alternative navigation
			try
			{
				if (sender is Button button && button.BindingContext is Contact contact)
				{
					await Navigation.PushAsync(new EditContactPage(contact));
				}
			}
			catch
			{
				// If no EditContactPage exists, show alert
				await DisplayAlert("Edit Contact", "Edit contact functionality will be implemented here.", "OK");
			}
		}
	}

	private void OnContactSelected(object sender, SelectionChangedEventArgs e)
	{
		// Handle contact selection
		if (e.CurrentSelection.FirstOrDefault() is Contact selectedContact)
		{
			// Handle selection logic
			DisplayAlert("Contact Selected", $"Selected: {selectedContact.Name}", "OK");
		}
	}

	private async void OnContactTapped(object sender, EventArgs e)
	{
		try
		{
			// Handle contact tap with animation
			var tappedView = sender as View;

			await tappedView.ScaleTo(0.95, 100);
			await tappedView.ScaleTo(1.0, 100);

			// Get the contact from the tapped view's binding context
			if (tappedView.BindingContext is Contact selectedContact)
			{
				// Navigate to edit contact page
				await Shell.Current.GoToAsync($"//editcontact?contactId={selectedContact.Name}");
			}
		}
		catch (Exception ex)
		{
			// If Shell navigation fails, try alternative navigation
			try
			{
				if (sender is View view && view.BindingContext is Contact contact)
				{
					await Navigation.PushAsync(new EditContactPage(contact));
				}
			}
			catch
			{
				// If no EditContactPage exists, show contact details in alert
				if (sender is View tappedView && tappedView.BindingContext is Contact contact)
				{
					await DisplayAlert("Contact Details",
						$"Name: {contact.Name}\nPhone: {contact.PhoneNumber}\nEmail: {contact.Email}",
						"OK");
				}
			}
		}
	}

	private async void OnCallClicked(object sender, EventArgs e)
	{
		var button = sender as Button;

		// Animate button
		await button.ScaleTo(0.8, 100);
		await button.ScaleTo(1.0, 100);

		// Implement call functionality
		// PhoneDialer.Open(phoneNumber);
	}

	private async void OnMessageClicked(object sender, EventArgs e)
	{
		var button = sender as Button;

		// Animate button
		await button.ScaleTo(0.8, 100);
		await button.ScaleTo(1.0, 100);

		// Implement message functionality
		// Sms.ComposeAsync(new SmsMessage("", phoneNumber));
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		// Initialize animations when page appears
		AnimatePageEntry();
	}

	private async void AnimatePageEntry()
	{
		// Initial state
		headerSection.Opacity = 0;
		headerSection.TranslationY = -50;
		contactsCollectionView.Opacity = 0;
		contactsCollectionView.TranslationY = 30;

		// Animate in
		await Task.WhenAll(
			headerSection.FadeTo(1, 400, Easing.CubicOut),
			headerSection.TranslateTo(0, 0, 400, Easing.CubicOut),
			contactsCollectionView.FadeTo(1, 500, Easing.CubicOut),
			contactsCollectionView.TranslateTo(0, 0, 500, Easing.CubicOut)
		);
	}
}

// Sample Contact model (if not already defined)
public class Contact
{
	public string Name { get; set; }
	public string PhoneNumber { get; set; }
	public string Email { get; set; }
	public string Avatar { get; set; }
	public bool IsFavorite { get; set; }
	public bool IsOnline { get; set; }
}
