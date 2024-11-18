# Municipal Services Application

## Overview
The Municipal Services Application is a platform designed for South African citizens to report municipal issues and access local events and announcements. The application enhances user engagement by offering features like issue reporting, event recommendations, and tracking service requests.

## Features
1. **Report Issues**: Users can report municipal issues (e.g., sanitation, roads, utilities) by providing details such as location, category, description, and attachments.
   
2. **Local Events and Announcements**: View and search upcoming local events based on categories and dates.

3. **Service Request Tracking**: Users can track the status of their submitted service requests (upcoming feature).

4. **Recommendations**: Suggests related events based on user search patterns through a content-based recommendation system.

## Technical Details

### Technologies Used
- **Language**: C#
- **Framework**: .NET Framework
- **Data Structures**:
  - **Queues**: Manage events in chronological order.
  - **Sorted Dictionaries**: Efficiently retrieve events by date.
  - **Hash Sets**: Store unique event categories.
  - **Dictionaries**: Track user search patterns for event recommendations.

### System Requirements
- **Operating System**: Windows 10 or later
- **Software**: Visual Studio 2019 or later, .NET Framework 4.7.2 or later
- **Hardware**: 4 GB RAM, Intel Core i3 or equivalent, 200 MB disk space

### File Structure
```
MunicipalServicesApp/
├── MainForm.cs            # Main menu for the application
├── ReportIssuesForm.cs    # Form for reporting issues
├── LocalEventsForm.cs     # Form for events and announcements
├── EventManager.cs        # Class for managing events
├── EventListPop.cs        # Class for managing unique event categories
├── RecommendationSystem.cs# Event recommendation system
├── README.md              # This file
└── ...
```

## Recommendation System

- **User Search History**: Stores user searches to personalize event recommendations.
- **Simple Neural Network**: Learns from user search history and random events using gradient descent.
- **Content-Based Filtering**: Recommends events based on their features (e.g., category, date) and past user interactions.
- **Event Recommendations**: Uses a sigmoid function to rank events and return the most relevant ones.

## Class Overview

### `User`
- Manages a user's search history.
- Methods:
  - `AddSearch(Event eventItem)`: Adds an event to the user’s search history.

### `SimpleNeuralNet`
- A neural network that predicts user interest in events.
- Methods:
  - `Train(User user, Event[] allEvents, Dictionary<string, int> categoryMap, int epochs)`: Trains on positive (search history) and negative (random) events.
  - `Predict(double[] featureVector)`: Predicts event relevance using a sigmoid activation function.
  - `RecommendEvents(User user, Event[] allEvents, Dictionary<string, int> categoryMap, int topN)`: Recommends top-N events based on the prediction scores.

## How It Works

1. **Training**: 
   - The neural network trains on a user’s search history and random events to learn their preferences.
   
2. **Event Features**: 
   - Events are converted to feature vectors using a predefined **category map** (e.g., event type, date).
   
3. **Recommendations**: 
   - The model predicts event relevance and recommends the top-N events based on those predictions.

### How to Compile and Run
1. **Clone the Repository**:
   ```bash
   git clone https://github.com/IIEWFL/prog7312-part-2-ST10119175.git
   ```

2. **Open in Visual Studio**:
   - Launch Visual Studio and open the `MunicipalServicesApp.sln` file.

3. **Build the Solution**:
   - Press `Ctrl + Shift + B` to build the solution.

4. **Run the Application**:
   - Press `F5` to run the app.

## Usage
1. **Report Issues**: Use the report form to submit municipal issues with attachments.
2. **View Events**: Check local events and announcements. Filter by categories and dates.
3. **Receive Recommendations**: Get personalized event suggestions based on your search history.

## License
This project is licensed under the MIT License. See the LICENSE file for more details.


[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/WcLxskuS)


[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/BbhbQeE4)
