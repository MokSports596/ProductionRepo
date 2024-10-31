# Mok Fantasy Football Application

**Mok** is a fantasy football application that provides users with an engaging platform to manage their fantasy teams, make weekly picks, and track performance in real-time. The app features a **React Native** frontend and a **C#/.NET backend** hosted on **AWS EC2**. For any questions about API keys and other access info please reach out.

## Features

- **User Authentication**: Secure login and registration.
- **Team Management**: Create and manage fantasy teams.
- **Live Updates**: Integration with live sports APIs for real-time scores and updates.
- **Trades & Free Agency**: Manage team trades and free agency moves.
- **Leaderboard**: Displays user rankings and performance across the season.

---

## Tech Stack

- **Frontend**: React Native (JavaScript)
- **Backend**: C#, .NET Core
- **Database**: Amazon RDS (SQL Server)
- **Hosting**: Backend hosted on AWS EC2, Frontend deployed to TestFlight (iOS)

---

## Prerequisites

Before you begin, ensure you have the following installed:

- **Node.js** (>= 14.x)
- **npm** (comes with Node.js)
- **.NET Core SDK** (>= 6.x)
- **React Native CLI** (if developing locally)
- **Xcode** (for iOS development)
- **Android Studio** (for Android development)

---

## Quick Setup and Installation

### 1. Clone the Repository

```bash
git clone https://github.com/<your-username>/<your-repo>.git](https://github.com/MokSports596/ProductionRepo.git
cd ProductionRepo
```

### 2. Backend Setup

1. Navigate to the backend directory:
   ```bash
   cd Backend
   ```

2. Set up your environment variables by creating an `.env` file. Example:
   ```
   NFL_API_KEY=<your_nfl_api_key>
   DB_CONNECTION_STRING=Server=<db-server>;Database=<db-name>;User ID=<db-user>;Password=<db-password>;
   ```

3. Install the necessary dependencies and run the backend:
   ```bash
   dotnet restore
   dotnet run
   ```

   The backend will run on `http://localhost:5062`.


### 3. Frontend Setup

1. Navigate to the **frontend** directory:
   ```bash
   cd Frontend
   ```

2. Install the dependencies:
   ```bash
   npm install
   ```

3. Run the app on iOS or Android:
   - **iOS** (requires macOS):
     ```bash
     npx react-native run-ios
     ```

   - **Android**:
     ```bash
     npx react-native run-android
     ```

4. Ensure the `.env` file is set up in the frontend folder with the following format:
   ```
   API_BASE_URL=http://<your-ec2-ip>:5062/api
   ```

---

## Running Tests

### Backend Tests
To run the backend tests, navigate to the `Backend` folder and run:

```bash
dotnet test
```

---

## Contributing

If you'd like to contribute to this project, please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature (`git checkout -b feature-branch`).
3. Commit your changes (`git commit -m "Add some feature"`).
4. Push to the branch (`git push origin feature-branch`).
5. Open a pull request.

---

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

### Full README Explanation:
- **Quick Setup and Installation**: Provides clear instructions to get both the backend and frontend up and running.
- **Environment Variables**: Details what needs to go in the `.env` files for both the backend and frontend.
- **Running Tests**: Instructions on how to run tests for the backend.
- **Contributing**: Basic guidelines for contributors.
- **License**: Specifies the licensing for the project.
