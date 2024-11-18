# Municipal Services Application

## Overview
The Municipal Services Application is a platform for South African citizens to report municipal issues and access local events and announcements. It enhances user engagement by offering features like issue reporting, event recommendations, and service request tracking.

## Features
1. **Report Issues**: Users can report municipal issues (e.g., sanitation, roads, utilities) by providing details such as location, category, description, and attachments.
   
2. **Local Events and Announcements**: View and search upcoming local events based on categories and dates.

3. - **Service Request Tracking (New Feature):** Users can track the status of their submitted service requests. This feature utilizes various data structures such as binary trees, AVL trees, heaps, graphs, and hash tables for efficient management and retrieval of service requests.

4. **Recommendations**: Suggests related events based on user search patterns through a content-based recommendation system.

## Technical Details

### Technologies Used
- **Language**: C#
- **Framework**: .NET Framework
- **Data Structures**:
 - **Queues:** Manage events in chronological order.
  - **Sorted Dictionaries:** Efficiently retrieve events by date.
  - **Hash Sets:** Store unique event categories.
  - **Dictionaries:** Track user search patterns for event recommendations.
  - **Binary Trees (AVL Trees):** Organize service requests by criteria such as priority and timestamp for efficient searching and retrieval.
  - **Heaps (Priority Queues):** Manage service requests based on priority for quick access to the most urgent tasks.
  - **Graphs:** Represent relationships between service requests, workers, and locations for optimized pathfinding and resource allocation.
  - **Hash Tables:** Store and quickly retrieve service request statuses by unique identifiers.

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
├── ServiceRequestStatusForm.cs # Manages service request tracking
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
   git clone https://github.com/IIEWFL/prog7312-poe-ST10119175.git
   ```

2. **Open in Visual Studio**:
   - Launch Visual Studio and open the `MunicipalServicesApp.sln` file.

3. **Build the Solution**:
   - Press `Ctrl + Shift + B` to build the solution.

4. **Run the Application**:
   - Press `F5` to run the app.


## Loading the Service Requests Data

To load the service request data into the application, follow these steps:

1. **Prepare the CSV File**:
   - Ensure you have a `ServiceRequests.csv` file containing the data you want to load into the application. The CSV should follow this basic format:
     ```
     RequestID, IssueCategory, Description, Location, Status, DateReported
     1, Sanitation, Blocked drain, Newtown, Pending, 2024-11-01
     2, Roads, Pothole repair, Hillview, Completed, 2024-11-03
     ...
     ```
   - The file should be formatted with the following columns:
     - `RequestID`: Unique identifier for the service request.
     - `IssueCategory`: Category of the issue (e.g., Sanitation, Roads, etc.).
     - `Description`: Description of the reported issue.
     - `Location`: Location of the issue.
     - `Status`: Current status of the service request (e.g., Pending, Completed).
     - `DateReported`: Date the request was reported.

2. **Load the Data in the Application**:
   - The `ServiceRequests.csv` can be loaded into the application via the `ServiceRequestManager` class.
   - You can add this functionality by calling the `LoadServiceRequests` method with the file path of the CSV.

   Example:
   ```csharp
   ServiceRequestManager.LoadServiceRequests("path_to_your_file/ServiceRequests.csv");
   ```

3. **Method Overview**:
   - The `LoadServiceRequests` method reads the CSV file, parses each row, and creates a `ServiceRequest` object for each entry.
   - The data is then added to an internal collection (e.g., `List<ServiceRequest>`) for further processing and display in the application.

4. **Verify the Data**:
   - After loading, you can check the list of service requests via the “Service Request Tracking” feature in the app.
   - The requests will be displayed with their details, and you can track their current status or view more details about each one.


## Usage
1. **Report Issues**: Use the report form to submit municipal issues with attachments.
2. **View Events**: Check local events and announcements. Filter by categories and dates.
3. **Receive Recommendations**: Get personalized event suggestions based on your search history.
4. - **Track Service Requests:** View the status of submitted service requests.

## License
This project is licensed under the MIT License. See the LICENSE file for more details.



[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/BbhbQeE4)
