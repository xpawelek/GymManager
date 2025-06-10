<h1> GymManager </h1>

GymManager to kompleksowy system zarządzania siłownią, zbudowany z użyciem ASP.NET Core (API + Blazor), Entity Framework Core, JWT, NLog oraz CI/CD na GitHub Actions.

System obsługuje:

zarządzanie sprzętem, trenerami, członkostwami,

przesyłanie zdjęć postępów,

wiadomości między członkiem a trenerem,

zgłoszenia serwisowe,

logowanie i rejestrację,

raportowanie i testy wydajności.

📁 Struktura projektu
GymManager.Client/ – Blazor WebAssembly (SPA)

GymManager/ – ASP.NET Core Web API

GymManager.Tests/ – testy (xUnit, Moq, FluentAssertions)

GymManager.Shared/ – DTO, enumy, typy wspólne

GymManager.Models/ – modele EF, mapery

GymManager.Services/ – logika (Admin, Member, Trainer)

GymManager.Data/ – EF Core DbContext + migracje

🧠 Kluczowe funkcje
🔐 Role i autoryzacja
Role: Admin, Member, Trainer

JWT autoryzacja (brak cookies)

Oddzielone kontrolery: AuthApiController (API), AuthController (Razor)

🏋️ Sprzęt i trenerzy
Admin może dodawać/edyto-wać zdjęcia sprzętu i trenerów

Publiczna prezentacja w /equipment, /trainers

📸 Zdjęcia postępów
Członkowie przesyłają zdjęcia tylko przy aktywnym członkostwie

Trenerzy widzą postępy przypisanych członków

Edycja i komentarze inline

💬 Wiadomości
Komunikacja między członkiem a przypisanym trenerem

Komponenty: MessagesMember.razor, MessagesTrainer.razor

Styl czatu (kolor + wyrównanie)

🛠️ Zgłoszenia serwisowe
Składanie problemów z sprzętem na siłowni. Komunikacja między trenerem/memberem a adminem.
Admin może zaznaczać naprawione notki flagą Done.

🧪 Testy jednostkowe
Testy jednostkowe zrealizowano przy użyciu:

xUnit

Zakres testów:

Serwisy logiki biznesowej (np. MemberSelfWorkoutNoteService, TrainerSelfMessageService)

Pokrycie scenariuszy pozytywnych i błędów

🔄 CI/CD – GitHub Actions
Zaprojektowano pipeline CI/CD w pliku .github/workflows/dotnet-ci.yml.

Etapy:

Restore: dotnet restore

Build: dotnet build --configuration Release

Test: dotnet test

Dzięki temu każda zmiana w repozytorium automatycznie przechodzi przez proces budowania i testowania.

📎 Workflow: dotnet-ci.yml

🧱 Indeksy – optymalizacja zapytań SQL
Zoptymalizowano dwa często wykonywane zapytania poprzez dodanie nieklastrowanych indeksów:

IX_Memberships_MemberId_IsActive

IX_TrainerAssignments_TrainerId_MemberId

Analiza:

PRZED: Zapytania powodowały Clustered Scan, co generowało wysoki koszt (np. sortowanie po StartTime)

PO: Dzięki indeksom możliwe było użycie Index Seek, zmniejszenie liczby odczytów i poprawa czasu wykonania

📎 Plik: raport-indeksy.pdf

📨 Logowanie (NLog)
Konfiguracja w nlog.config tworząca plik errors.log w binie

📆 Usługa raportowa
OpenOrderReportBackgroundService.cs generuje codzienny raport PDF i wysyła go na email admina

📚 Dokumentacja API
Endpointy główne (przykładowe):

Metoda	Endpoint	Opis
POST	/api/auth/login	Logowanie
POST	/api/auth/register	Rejestracja
GET	/api/equipment	Pobierz sprzęt
POST	/api/progress	Dodaj zdjęcie postępów
GET	/api/messages	Pobierz wiadomości
POST	/api/service-requests	Zgłoszenie serwisowe

i wiele wiele więcej ...
