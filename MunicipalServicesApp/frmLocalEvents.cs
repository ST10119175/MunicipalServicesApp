using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class frmLocalEvents : Form
    {

       // https://www.w3schools.com/dsa/index.php
       //Jamro, M. 2018. C# Data Structures and Algorithms
       // Malik, D.S. (2018). C++ programming : program design including data structures. 6th ed.Boston, Ma: Cengage Learning.

        // Queue for managing service requests
        private Queue<ServiceRequest> serviceQueue = new Queue<ServiceRequest>();

        // Dictionary for storing events by categories
        //private Dictionary<string, List<Event>> eventCategories = new Dictionary<string, List<Event>>();

        // Stack for navigating through viewed events
        private Stack<Event> viewedEventsStack = new Stack<Event>();

        // Set to store unique event categories
        private HashSet<string> eventCategorySet = new HashSet<string>();

        // SortedDictionary for storing events by categories (sorted by category name)
        private SortedDictionary<string, List<Event>> eventCategories = new SortedDictionary<string, List<Event>>();

        // List of all events
        private List<Event> eventList = new List<Event>();

        // Constants for UI text
        private const string SearchPlaceholder = "Search for events...";

        private bool sortAscendingName = true; // Track sorting order for name
        private bool sortAscendingDate = true; // Track sorting order for date
        private bool sortAscendingCategory = true; // Track sorting order for category


        private HashSet<string> uniqueCategories = new HashSet<string>();
        private HashSet<DateTime> uniqueDates = new HashSet<DateTime>();


        // List to store user search history
        private List<string> searchHistory = new List<string>();



        public frmLocalEvents()
        {
            InitializeComponent();
            InitializeCustomLogic();
        }


        
        private void InitializeCustomLogic()
        {
            PopulateEventList();
            PopulateListView(eventList);
            PopulateCategoryComboBox();
            SetupEventHandlers();
            InitializeSearchBox();
            InitializeListView();

            InitializeNeuralNetwork(); // Initialize the neural network here

            dateTimePicker1.ShowCheckBox = true;

            listViewEvents.ColumnClick += ListViewEvents_ColumnClick; // Subscribe to ColumnClick event

            

        }


        //ML implantation here 

        // Declare the necessary fields for the neural network
        private Dictionary<string, int> categoryMap = new Dictionary<string, int>();
        private SimpleNeuralNet neuralNetwork;
        private User currentUser;

        private void InitializeNeuralNetwork()
        {
            // Assuming 5 features (e.g., category one-hot encoding + normalized date)
            neuralNetwork = new SimpleNeuralNet(learningRate: 0.01);
            currentUser = new User(userId: 1);  // Initialize with user ID 1 (can be dynamic)
        }

        //end 




        private async void PerformSearch()
        {
            // Use Task.Run to run the search asynchronously
            await Task.Run(() =>
            {

                //string searchText = txtSearch.Text.ToLower();

                // Trim the search text to remove any leading/trailing whitespace
                string searchText = txtSearch.Text.Trim().ToLower();

                string selectedCategory = null;

                // Invoke to safely access UI controls
                this.Invoke(new Action(() =>
                {
                    // Ignore the placeholder text
                    if (searchText == SearchPlaceholder.ToLower())
                    {
                        searchText = null;
                    }

                    // Get selected category from comboBox
                    selectedCategory = comboBoxCategories.SelectedItem as string;
                }));

                // Invoke to safely access UI controls
                this.Invoke(new Action(() =>
                {
                    // If the search box contains the placeholder, we ignore the text search
                    if (searchText == SearchPlaceholder.ToLower())
                    {
                        searchText = null;
                    }

                    if (!string.IsNullOrEmpty(searchText) && searchText != SearchPlaceholder.ToLower())
                    {
                        if (!searchHistory.Contains(searchText))
                        {
                            searchHistory.Add(searchText);
                        }

                        else
                        {
                            searchHistory.Add("goku");
                            searchHistory.Add("sports");
                        }
                    }
                }));

                //ml stuff 
                if (searchText != SearchPlaceholder.ToLower())
                {
                    // Add search term to history
                    if (!searchHistory.Contains(searchText))
                        searchHistory.Add(searchText);
                }


                DateTime selectedDate = dateTimePicker1.Value.Date;
                bool isDateFilterChecked = false;

                // Invoke to safely access UI controls
                this.Invoke(new Action(() => 
                { 
                isDateFilterChecked = dateTimePicker1.Checked; // Check if the date filter is enabled
                }));
                

                // Filter events based on search criteria
                var filteredEvents = eventList.Where(evt =>
                     (string.IsNullOrEmpty(searchText) || evt.Name.ToLower().Contains(searchText)) &&
            (selectedCategory == "All Categories" || evt.Category == selectedCategory) &&
            (!isDateFilterChecked || evt.Date.Date == selectedDate) // Apply date filter only if checked
                ).ToList();



                //ml 

                // Invoke UI updates back on the main thread
                this.Invoke(new Action(() =>
                {
                    // Add the events to the user's search history as training data
                    foreach (var evt in filteredEvents)
                {
                    currentUser.AddSearch(evt);
                }

                // Populate the ListView with the filtered events
                PopulateListView(filteredEvents);

                // Display a message with the search results
                string message = $"Displaying events";
                if (!string.IsNullOrEmpty(searchText))
                    message += $" containing '{searchText}'";
                if (selectedCategory != "All Categories")
                    message += $" in category '{selectedCategory}'";
                if (selectedDate != DateTime.MinValue)
                    message += $" on {selectedDate.ToString("dd MMM yyyy")}";

                MessageBox.Show($"{message}\nFound {filteredEvents.Count} event(s)", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }));
            });
        }


        // Method to populate the ListView with events
        private void PopulateListView(IEnumerable<Event> events, string label = "Search Results", User user=null)
        {


            listViewEvents.Items.Clear();

            foreach (var e in events)
            {
                var item = new ListViewItem(new[]
                {
                e.Name,
                e.Date.ToString("dd MMM yyyy"),
                e.Category
            });
                listViewEvents.Items.Add(item);
            }

            MessageBox.Show($"{label}: Found {events.Count()} event(s)", label, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Recommendation feature based on user's previous searches
        private async void btnRecommend_Click(object sender, EventArgs e)
        {

            // Disable the button to prevent multiple clicks during processing
            btnRecommend.Enabled = false;


            // Train the neural network asynchronously
            await Task.Run(() => neuralNetwork.Train(currentUser, eventList.ToArray(), categoryMap, epochs: 1000));


            // Get recommendationstop 10 events using the neural network asynchronously
            var mlrecommendedEvents = await Task.Run(() =>
                neuralNetwork.RecommendEvents(currentUser, eventList.ToArray(), categoryMap, topN: 10));

            // Display the recommended events
            PopulateListView(mlrecommendedEvents, " neural network Recommended", currentUser);


            // provide other recommendations from random categories as a fallback
            var randomCategory = eventCategorySet.OrderBy(_ => Guid.NewGuid()).FirstOrDefault();

            try
            {
              
                if (!string.IsNullOrEmpty(randomCategory) && eventCategories.TryGetValue(randomCategory, out var recommendedEvents))
                {
                    PopulateListView(recommendedEvents);
                    MessageBox.Show($"Recommended random events from category: {randomCategory}", "Recommendations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No recommendations available.", "Recommendations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowError("Error generating recommendations", ex);
            }

            finally
            {
                // Re-enable the button after processing
                btnRecommend.Enabled = true;
            }


            RecommendEventsBasedOnSearchHistory();


        }





        private Dictionary<string, int> AnalyzeSearchHistory()
        {
            var searchFrequency = new Dictionary<string, int>();

            // Count occurrences of search terms
            foreach (var term in searchHistory)
            {
                if (!string.IsNullOrEmpty(term))
                {
                    if (searchFrequency.ContainsKey(term))
                    {
                        searchFrequency[term]++;
                    }
                    else
                    {
                        searchFrequency[term] = 1;
                    }
                }
            }

            return searchFrequency;
        }


        //frequency analysis of search history custom algorithm (Frequency-based relevance)
        private void RecommendEventsBasedOnSearchHistory()
        {
            var searchFrequency = AnalyzeSearchHistory();

            // Find the most searched term (most frequent)
            string mostFrequentSearch = searchFrequency.OrderByDescending(kvp => kvp.Value).FirstOrDefault().Key;

            // Find the top 3 most searched terms (if available)
            var mostFrequentSearchTerms = searchFrequency.OrderByDescending(kvp => kvp.Value)
                                                          .Take(3)
                                                          .Select(kvp => kvp.Key)
                                                          .ToList();


            // List to accumulate recommended events based on frequent terms
            var recommendedEvents = new List<Event>();

            // Ensure mostFrequentSearch is not null or empty
            if (!string.IsNullOrEmpty(mostFrequentSearch))
            {

                //// Recommend events that match the most frequent search term
                // Collect events that match the top search terms
                foreach (var searchTerm in mostFrequentSearchTerms)
                {
                    var eventsMatchingTerm = eventList.Where(evt =>
                        evt.Name.ToLower().Contains(searchTerm) ||
                        evt.Category.ToLower().Contains(searchTerm)).ToList();

                    recommendedEvents.AddRange(eventsMatchingTerm);
                }


                if (recommendedEvents.Any())
                {
                    PopulateListView(recommendedEvents, mostFrequentSearch);
                    MessageBox.Show($"Recommended events based on your interest in (custom algorithm) '{mostFrequentSearch}'", "Recommendations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No recommendations available based on your search history.", "Recommendations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No frequent search term found. Please search for some events first.", "Recommendations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void PopulateListView(IEnumerable<Event> events,string searched)
        {
            listViewEvents.Items.Clear();

           
            

            foreach (var e in events)
            {
                var item = new ListViewItem(new[]
                {
            e.Name,
            e.Date.ToString("dd MMM yyyy"),
            e.Category
        });
                listViewEvents.Items.Add(item);
            }
        }




        private void materialButton1_Click(object sender, EventArgs e)
        {
            PopulateUniqueCategoriesAndDates(); // Populate unique categories and dates


            EventListPopup popup = new EventListPopup(uniqueCategories, uniqueDates);
            popup.ShowDialog(); // Show the popup as a dialog
        }



        // Method to populate unique categories and dates
        private void PopulateUniqueCategoriesAndDates()
        {
            foreach (var e in eventList)
            {
                uniqueCategories.Add(e.Category);
                uniqueDates.Add(e.Date);
            }

           
        }

       
        // ColumnClick event handler for sorting
        private void ListViewEvents_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Sort the ListView based on the clicked column
            switch (e.Column)
            {
                case 0: // Name column
                    SortListViewByName(sortAscendingName);
                    sortAscendingName = !sortAscendingName; // Toggle sorting order
                    break;
                case 1: // Date column
                    SortListViewByDate(sortAscendingDate);
                    sortAscendingDate = !sortAscendingDate; // Toggle sorting order
                    break;
                case 2: // Category column
                    SortListViewByCategory(sortAscendingCategory);
                    sortAscendingCategory = !sortAscendingCategory; // Toggle sorting order
                    break;
            }
        }


        // Method to sort ListView by name
        private void SortListViewByName(bool ascending)
        {
            var sortedItems = listViewEvents.Items.Cast<ListViewItem>()
                .OrderBy(item => item.SubItems[0].Text) // Sort by name (1st column)
                .ToList();

            if (!ascending)
            {
                sortedItems.Reverse(); // Reverse order for descending
            }

            listViewEvents.Items.Clear(); // Clear current items
            listViewEvents.Items.AddRange(sortedItems.ToArray()); // Add sorted items back
        }

        // Method to sort ListView by date
        private void SortListViewByDate(bool ascending)
        {
            var sortedItems = listViewEvents.Items.Cast<ListViewItem>()
                .OrderBy(item => DateTime.Parse(item.SubItems[1].Text)) // Sort by date (2nd column)
                .ToList();

            if (!ascending)
            {
                sortedItems.Reverse(); // Reverse order for descending
            }

            listViewEvents.Items.Clear(); // Clear current items
            listViewEvents.Items.AddRange(sortedItems.ToArray()); // Add sorted items back
        }



        // Method to sort ListView by category
        private void SortListViewByCategory(bool ascending)
        {
            var sortedItems = listViewEvents.Items.Cast<ListViewItem>()
                .OrderBy(item => item.SubItems[2].Text) // Sort by category (3rd column)
                .ToList();

            if (!ascending)
            {
                sortedItems.Reverse(); // Reverse order for descending
            }

            listViewEvents.Items.Clear(); // Clear current items
            listViewEvents.Items.AddRange(sortedItems.ToArray()); // Add sorted items back
        }


        private void SetupEventHandlers()
        {
            btnSearch.Click += btnSearch_Click;
            btnSubmit.Click += btnSubmit_Click;
            comboBoxCategories.SelectedIndexChanged += comboBoxCategories_SelectedIndexChanged;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            listViewEvents.SelectedIndexChanged += listViewEvents_SelectedIndexChanged;
            btnBack.Click += btnBack_Click;
            btnRecommend.Click += btnRecommend_Click;
        }


        // search box initialization
        private void InitializeSearchBox()
        {
            txtSearch.Text = SearchPlaceholder;
            txtSearch.ForeColor = System.Drawing.Color.Gray;
            txtSearch.Enter += txtSearch_Enter;
            txtSearch.Leave += txtSearch_Leave;
        }


        // Initialize dummy event data
        private void PopulateEventList()
        {
            try
            {
                List<Event> events = SampleDataProvider.GetSampleEvents();


                // Adding events to the list, dictionary, and set
                foreach (var ev in events)
                {
                    eventList.Add(ev);
                    AddEventToCategory(ev);
                    eventCategorySet.Add(ev.Category);
                }
            }
            catch (Exception ex)
            {
                ShowError("Error populating event list", ex);
            }
        }

        private void InitializeListView()
        {
            // Set up columns
            listViewEvents.View = View.Details;
            listViewEvents.Columns.Add("Event Name", 200);
            listViewEvents.Columns.Add("Date", 100);
            listViewEvents.Columns.Add("Category", 100);

            // Enable full row select for better UX
            listViewEvents.FullRowSelect = true;
        }

        private void PopulateListView(IEnumerable<Event> events)
        {



            listViewEvents.Items.Clear();
            foreach (var e in events)
            {
                var item = new ListViewItem(new[]
                {
            e.Name,
            e.Date.ToString("dd MMM yyyy"),
            e.Category
        });
                listViewEvents.Items.Add(item);
            }
        }

        // Populate the category ComboBox
        private void PopulateCategoryComboBox()
        {
            comboBoxCategories.Items.Clear();
            comboBoxCategories.Items.Add("All Categories"); // Add an "All" option
            comboBoxCategories.SelectedIndex = 0; // Select "All Categories" by default

            // Add sorted categories to the ComboBox (SortedDictionary keeps them sorted)
            foreach (var category in eventCategories.Keys)
            {
                comboBoxCategories.Items.Add(category);
            }
        }

        // Method to add an event to its corresponding category in the dictionary
        private void AddEventToCategory(Event newEvent)
        {
          

            if (!eventCategories.TryGetValue(newEvent.Category, out var categoryEvents))
            {
                categoryEvents = new List<Event>();
                eventCategories[newEvent.Category] = categoryEvents;
            }
            categoryEvents.Add(newEvent);
        }

      

        // Event handler to view event details (push event onto stack)
        private void listViewEvents_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                if (listViewEvents.SelectedItems.Count > 0)
                {
                    string eventName = listViewEvents.SelectedItems[0].Text;
                    Event selectedEvent = eventList.FirstOrDefault(ev => ev.Name == eventName);

                    if (selectedEvent != null)
                    {
                        viewedEventsStack.Push(selectedEvent);
                        MessageBox.Show($"You are viewing: {selectedEvent.Name}", "Event Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Error viewing event details", ex);
            }

            //try
            //{
            //    if (listViewEvents.SelectedItems.Count > 0)
            //    {
            //        string eventName = listViewEvents.SelectedItems[0].SubItems[0].Text;
            //        Event selectedEvent = eventList.FirstOrDefault(ev => ev.Name == eventName);

            //        if (selectedEvent != null)
            //        {
            //            // Push viewed event onto the stack
            //            viewedEventsStack.Push(selectedEvent);
            //            MessageBox.Show($"You are viewing: {selectedEvent.Name}", "Event Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Error viewing event details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        // Button to go back to the last viewed event (pop from the stack)
        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (viewedEventsStack.Count > 0)
                {
                    Event lastViewedEvent = viewedEventsStack.Pop();
                    MessageBox.Show($"Returning to: {lastViewedEvent.Name}", "Navigation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No more events in the history.", "Navigation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ShowError("Error navigating back", ex);
            }
        }



        // Event handler to process service requests in the queue
        private void btnProcessRequest_Click(object sender, EventArgs e)
        {
            try
            {
                
            
                if (serviceQueue.Count > 0)
                {
                    ServiceRequest request = serviceQueue.Dequeue();
                    MessageBox.Show($"Processing request: {request.Description}", "Service Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No service requests to process.", "Service Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing service request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // Reset ListView when submit is clicked
        private void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {
                PopulateListView(eventList);
                ResetFilters();
            }
            catch (Exception ex)
            {
                ShowError("Error resetting view", ex);
            }
        }



        // Search button click handler
        private void btnSearch_Click(object sender, EventArgs e)
        {

            PerformSearch();
           
        }


        

        private void comboBoxCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            PerformSearch(); // Reapply filters when category changes
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            PerformSearch(); // Reapply filters when date changes
        }

        //// Category filter selection
        //private void comboBoxCategories_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        if (comboBoxCategories.SelectedItem is string selectedCategory)
        //        {
        //            var filteredEvents = eventList.Where(evt => evt.Category == selectedCategory).ToList();
        //            PopulateListView(filteredEvents);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error filtering by category: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //// Date filter selection
        //private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DateTime selectedDate = dateTimePicker1.Value.Date;
        //        var filteredEvents = eventList.Where(evt => evt.Date.Date == selectedDate).ToList();
        //        PopulateListView(filteredEvents);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error filtering by date: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        // New method to reset filters
        private void ResetFilters()
        {
            txtSearch.Text = SearchPlaceholder;
            txtSearch.ForeColor = System.Drawing.Color.Gray;
            comboBoxCategories.SelectedIndex = 0; // Reset to "All Categories"
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker1.Checked = false; // Uncheck the date filter
            PopulateListView(eventList); // Show all events
        }


        // Event handler for TextBox Enter event (removes placeholder text)
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == SearchPlaceholder)
            {
                txtSearch.Text = string.Empty;
                txtSearch.ForeColor = System.Drawing.Color.Black;
            }
        }

        // Event handler for TextBox Leave event (adds placeholder text back)
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = SearchPlaceholder;
                txtSearch.ForeColor = System.Drawing.Color.Gray;
            }
        }

        // Centralized error handling
        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}
    }


}