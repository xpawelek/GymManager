# 🏋️ GymManager

GymManager is a comprehensive full-stack application designed to manage a fitness facility. The system supports daily gym operations such as managing memberships, equipment, training sessions, staff, and communication between trainers and clients. It provides tailored views and functionalities for specific user roles.

## 🧰 Features

### User Registration and Management
- Registration of members, trainers, and receptionists
- JWT-based authentication with role management
- Editing personal data and managing assignments

### Training and Memberships
- Viewing, purchasing, and editing membership plans
- Creating and managing group and individual training sessions
- Adding workout notes to training sessions

### Multimedia
- Uploading progress and equipment photos
- Public and private photo visibility

### Communication
- Messaging system between trainers and members
- Conversation history
- Chat-style interface

### Equipment Service
- Equipment issue reporting system
- Assigning reports to specific equipment
- Generating PDF reports of open service requests

### Periodic Reports
- Monthly PDF reports generated and sent via email
- Includes stats like new memberships and service issues

## 🛠️ Technologies

- ASP.NET Core 8.0 (Web API)
- Entity Framework Core + SQL Server
- Blazor WebAssembly (frontend)
- GitHub Actions (CI)
- PDFSharpCore (report generation)
- JWT, Identity

## 📷 Screenshots

Below are some key views from the GymManager application:


### Main Page

![image](https://github.com/user-attachments/assets/efe7632b-eefc-46a7-b201-ef198eaa2601)
![image](https://github.com/user-attachments/assets/7f3c80b2-d1bb-49a1-9583-031562980fe4)
![image](https://github.com/user-attachments/assets/78db3dd3-dfe2-4b30-bad5-b11c5f8179b1)
![image](https://github.com/user-attachments/assets/1a8d276d-ada4-4281-b9b1-92a73ee8d354)
![image](https://github.com/user-attachments/assets/c631f63f-8a7f-4653-8b44-a897afb7c21e)



### "Progress" Tab

![image](https://github.com/user-attachments/assets/4bab52f8-0430-43f5-8e3b-63e9e7980e30)




### "Offers" Tab

![image](https://github.com/user-attachments/assets/9749e10f-caae-4184-86fa-ca309b42f149)




### "Trainers" Tab

![image](https://github.com/user-attachments/assets/fecb7b06-8bf7-46ac-9a98-9d4c360c7e6d)



### Login Panel

![image](https://github.com/user-attachments/assets/a33194c8-7a30-4031-9b25-c634ec4a8da1)



### Selected components from the admin management panel

![image](https://github.com/user-attachments/assets/0fab499a-353e-4323-b8f3-60a4fd70733d)

![image](https://github.com/user-attachments/assets/6b9f2156-da76-4cf1-aa56-4b7f9ddaf7e4)

![image](https://github.com/user-attachments/assets/1cd03d44-c90a-47a0-8de8-a32d41cb1756)

![image](https://github.com/user-attachments/assets/8d120d9c-41b0-4c13-8694-73d31d3e10ec)



### Selected components from the user management panel

![image](https://github.com/user-attachments/assets/1a3bb8be-be6d-40bc-923f-e3d943abaf0f)

![image](https://github.com/user-attachments/assets/8f549d91-374e-428a-a482-5e409fa3045b)

![image](https://github.com/user-attachments/assets/00599cf7-8318-4fea-9a41-f7a28afbaad8)

![image](https://github.com/user-attachments/assets/745c9c68-8e12-4e18-9d90-46ee1c842f12)




### Selected components from the trainer management panel

![image](https://github.com/user-attachments/assets/6d92b78b-7a73-4aa2-9286-3df4946989d8)

![image](https://github.com/user-attachments/assets/f6e75b7f-e460-4478-91e5-e172efaf98bb)

![image](https://github.com/user-attachments/assets/0090503f-924f-47a8-956e-6a043a8f72ab)

![image](https://github.com/user-attachments/assets/8298fd74-b100-4bf5-b55e-8d4a20a88a1a)




## 🚀 How to Run the Project Locally

1.  **Restore the database**
   - Open `SQL Server Management Studio`
   - Import the `gym-manager-dump.sql` file

2.  **Configure `appsettings.json`**
   - Set the connection string for the database
   - Add SMTP credentials (e.g. Gmail)

3. **Run the backend**
   ```bash
   dotnet run --project GymManager.GymManager

4. **Run the frontend**
   ```bash
   dotnet run --project GymManager.Client

5. 🔑 **Default admin credentials**

  *Email*: admin@gmail.com

  *Password*: Admin123!








