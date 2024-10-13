using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesApp
{


    // Event class to store event details
    public class Event
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }

        public Event(string name, string category, DateTime date)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Category = category ?? throw new ArgumentNullException(nameof(category));
            Date = date;
        }


        //content-based recommendation systems  machine learning application 
        //www.codeproject.com/Articles/5375908/Truly-Understanding-Neural-Networks-through-their)


        // Convert event to a feature vector (with dynamic one-hot encoding)
        public double[] ToFeatureVector(Dictionary<string, int> categoryMap)
        {
            double[] categoryEncoding = CategoryToOneHot(Category, categoryMap);
            double normalizedDate = NormalizeDate(Date);

            // Combine category encoding and date as a feature vector
            return categoryEncoding.Concat(new double[] { normalizedDate }).ToArray();
        }

        // Dynamically generate one-hot encoding for categories
        private double[] CategoryToOneHot(string category, Dictionary<string, int> categoryMap)
        {
            // Assign an index to the category if it is new
            if (!categoryMap.ContainsKey(category.ToLower()))
            {
                categoryMap[category.ToLower()] = categoryMap.Count;
            }

            // Create a one-hot vector of the current size of categoryMap
            double[] oneHotVector = new double[categoryMap.Count];
            int categoryIndex = categoryMap[category.ToLower()];

            // Set the category index to 1 in the one-hot vector
            oneHotVector[categoryIndex] = 1;

            return oneHotVector;
        }

        // Normalize date to a value between 0 and 1 (based on the year 2024)
        private double NormalizeDate(DateTime date)
        {
            DateTime start = new DateTime(2024, 1, 1);
            DateTime end = new DateTime(2024, 12, 31);
            double range = (end - start).TotalDays;
            return (date - start).TotalDays / range;
        }








    }

    // ServiceRequest class to manage service requests
    public class ServiceRequest
    {
        public string Description { get; set; }

        public ServiceRequest(string description)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}
