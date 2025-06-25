# Contacts.Maui

A beautiful, modern, cross-platform Contacts app built with .NET MAUI.

## Features

- 📱 Native UI for Android, iOS, macOS, and Windows
- 🖼️ Avatar support for each contact
- ⭐ Mark contacts as favorites
- 🟢 Online status indicator
- 🔍 Search and quick actions
- 📞 Call, message, and more options for each contact
- 🖌️ Professional, responsive design using Font Awesome icons
- 📝 Add, edit, and manage contacts

## Screenshots

![Contacts List Screenshot](docs/screenshot-contacts-list.png)

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022+ with MAUI workload

### Setup

1. **Clone the repository:**
   ```sh
   git clone https://github.com/yourusername/Contacts.Maui.git
   cd Contacts.Maui
   ```
2. **Restore dependencies:**
   ```sh
   dotnet restore
   ```
3. **Build and run:**
   - For Android:
     ```sh
     dotnet build -t:Run -f net8.0-android
     ```
   - For iOS (on Mac):
     ```sh
     dotnet build -t:Run -f net8.0-ios
     ```
   - For Mac Catalyst:
     ```sh
     dotnet build -t:Run -f net8.0-maccatalyst
     ```
   - For Windows:
     ```sh
     dotnet build -t:Run -f net8.0-windows10.0.19041.0
     ```

## Project Structure

- `Contacts.Maui/Views/` — All XAML pages (Contacts, Add, Edit)
- `Contacts.Maui/Models/` — Data models (Contact)
- `Contacts.Maui/Resources/` — Fonts, images, styles
- `Contacts.Maui/AppShell.xaml` — Shell navigation
- `Contacts.Maui/MauiProgram.cs` — App startup and font registration

## Customization

- Uses [Font Awesome Free](https://fontawesome.com/) for icons (see `Resources/Fonts/fa-solid-900.ttf`)
- Avatar images can be local or remote URLs
- Easily extendable for cloud sync, groups, etc.

## License

MIT License. See [LICENSE](LICENSE) for details.

---

> Built with ❤️ using .NET MAUI
