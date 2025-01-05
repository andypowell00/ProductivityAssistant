# Website Blocker

This application blocks or unblocks specified websites by modifying the `hosts` file on a Windows machine. It's designed to run as a scheduled task with arguments to block or unblock websites at specific times.

---

## **Setup Instructions**

### **1. Prerequisites**

- .NET SDK (version 6.0 or later)
- Administrator privileges (required to modify the `hosts` file)

---

### **2. Installation**

1. **Clone or Download the Repository**
   ```bash
   git clone <repository_url>
   cd <repository_folder>
   ```

2. **Build the Application**
   ```bash
   dotnet build -c Release
   ```

3. **Locate the Compiled Executable**
   The executable will be located in the `bin\Release\netX.X` directory, where `netX.X` corresponds to the .NET version used (e.g., `net6.0`, `net8.0`).

---

### **3. Configuration**

1. **Edit `settings.json`**
   Modify the `settings.json` file to configure the list of websites to block, the redirect IP, and the path to the `hosts` file:

   ```json
   {
     "HostsFilePath": "C:\\Windows\\System32\\drivers\\etc\\hosts",
     "RedirectIP": "127.0.0.1",
     "WebsitesToBlock": [
       "www.facebook.com",
       "facebook.com",
       "www.youtube.com",
       "youtube.com"
     ]
   }
   ```

2. **Ensure `settings.json` is Included in the Output Directory**
   Ensure the `settings.json` file is marked to copy to the output directory during build:

   - In Visual Studio, right-click `settings.json` > **Properties** > Set **Copy to Output Directory** to **Copy if newer**.

---

### **4. Run the Application**

You can run the application manually from the command line with the following arguments:

- `block`: Blocks the websites specified in `settings.json`.
- `unblock`: Unblocks the websites specified in `settings.json`.

#### Example:
```bash
<path_to_exe> block
<path_to_exe> unblock
```

---

## **Task Scheduler Setup**

### **1. Open Task Scheduler**

1. Press `Win + R`, type `taskschd.msc`, and press Enter.
2. In the left-hand menu, select **Task Scheduler Library**.
3. Click **Create Task...** in the right-hand menu.

### **2. General Settings**

- **Name**: `Website Blocker - Block` (or `Website Blocker - Unblock` for the unblock task).
- **Description**: Add a description to clarify the purpose of the task.
- Check **Run with highest privileges** (required for admin access).

### **3. Triggers**

- Click **New...**
- Configure the trigger based on your schedule:
  - **Start**: Set the date and time for the task to run.
  - **Daily**: Select daily recurrence.
  - **Repeat Task Every**: Optionally set a repeat interval (e.g., every day).

### **4. Actions**

- Click **New...**
- **Action**: Select **Start a program**.
- **Program/script**: Browse to the compiled executable.
- **Add arguments**:
  - For the blocking task: `block`
  - For the unblocking task: `unblock`

### **5. Conditions and Settings**

- In the **Conditions** tab, uncheck **Start the task only if the computer is on AC power** (optional, depending on preference).
- In the **Settings** tab, ensure **Allow task to be run on demand** is checked.

### **6. Save the Task**

Click **OK** to save the task. Enter your administrator credentials if prompted.

---

## **Testing the Setup**

1. Run the blocking task manually:
   - In Task Scheduler, right-click the blocking task and select **Run**.
2. Verify the `hosts` file:
   - Open `C:\Windows\System32\drivers\etc\hosts` and check if the websites are redirected to `127.0.0.1`.
3. Run the unblocking task manually:
   - Right-click the unblocking task and select **Run**.
4. Verify the `hosts` file again to ensure the changes are reverted.

---

## **Notes**

- Always run the application and tasks with administrator privileges.
- Task Scheduler is responsible for automating the execution of the application. Ensure your tasks are set up correctly.
- Keep the `settings.json` file secure as it contains configuration details.

---

If you encounter any issues, feel free to raise them in the repository's issue tracker.

