using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Chen, Y., Li, L., Li, W., Guo, Q., Du, Z. and Xu, Z. (2023).
//Fundamentals of neural networks. Elsevier eBooks, [online] pp.17–51. doi:https://doi.org/10.1016/b978-0-32-395399-3.00008-1.
//DESCARTES, N. (2024). Truly Understanding Neural Networks through their Implementation in C#. 
//[online] Codeproject.com. Available at: https://www.codeproject.com/Articles/5375908/Truly-Understanding-Neural-Networks-through-their) [Accessed 13 Oct. 2024].
//Frogglew (2024). What is Azure Machine Learning? - Azure Machine Learning. [online] learn.microsoft.com.
//Available at: https://learn.microsoft.com/en-us/azure/machine-learning/overview-what-is-azure-machine-learning?view=azureml-api-2.



//content-based recommendation systems  machine learning application 

namespace MunicipalServicesApp
{
    internal class content_based_recommendation_systems
    {
    }


    // User class for storing search history
    public class User
    {
        public int UserId { get; set; }
        public List<Event> SearchHistory { get; set; }

        public User(int userId)
        {
            UserId = userId;
            SearchHistory = new List<Event>();
        }

        public void AddSearch(Event eventItem)
        {
            SearchHistory.Add(eventItem);
        }
    }



    // Simple Neural Network for content-based filtering (for one user)
    public class SimpleNeuralNet
    {
        private double[] weights;
        private double bias;
        private double learningRate;

        public SimpleNeuralNet(double learningRate = 0.01)
        {
            this.learningRate = learningRate;
        }

        // Initialize weights based on input size
        private void InitializeWeights(int inputSize)
        {
            weights = new double[inputSize];
            bias = 0;
            Random rand = new Random();
            for (int i = 0; i < inputSize; i++)
            {
                weights[i] = rand.NextDouble() - 0.5; // Initialize with small random values
            }
        }

        // Train the model using search history (positive) and random events (negative)
        public void Train(User user, Event[] allEvents, Dictionary<string, int> categoryMap, int epochs)
        {
            // Ensure the weights are initialized based on the input size (categoryMap size + 1 for date)
            int inputSize = categoryMap.Count + 1;
            InitializeWeights(inputSize);

            // Convert events to feature vectors
            List<double[]> positiveSamples = user.SearchHistory.Select(e => e.ToFeatureVector(categoryMap)).ToList();
            List<double[]> negativeSamples = allEvents.Except(user.SearchHistory).Select(e => e.ToFeatureVector(categoryMap)).Take(positiveSamples.Count).ToList();

            for (int epoch = 0; epoch < epochs; epoch++)
            {
                foreach (var sample in positiveSamples)
                {
                    TrainSample(sample, 1); // Positive sample should have output = 1
                }

                foreach (var sample in negativeSamples)
                {
                    TrainSample(sample, 0); // Negative sample should have output = 0
                }
            }
        }

        private void TrainSample(double[] sample, double target)
        {
            double output = Predict(sample);
            double error = target - output;

            // Gradient Descent Update
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] += learningRate * error * sample[i];
            }
            bias += learningRate * error;
        }

        // Predict if the user will like an event (based on a sigmoid activation function)
        public double Predict(double[] featureVector)
        {
            double z = bias;

            // Weighted sum of features
            for (int i = 0; i < weights.Length; i++)
            {
                z += weights[i] * featureVector[i];
            }

            return Sigmoid(z);
        }

        private double Sigmoid(double z)
        {
            return 1.0 / (1.0 + Math.Exp(-z));
        }

        // Recommend events based on prediction scores
        public List<Event> RecommendEvents(User user, Event[] allEvents, Dictionary<string, int> categoryMap, int topN = 3)
        {
            // Ensure the weights are initialized based on the input size (categoryMap size + 1 for date)
            int inputSize = categoryMap.Count + 1;
            InitializeWeights(inputSize);

            // Generate scores for all events
            var eventScores = new List<Tuple<Event, double>>();
            foreach (var eventItem in allEvents)
            {
                double[] featureVector = eventItem.ToFeatureVector(categoryMap);
                double score = Predict(featureVector);
                eventScores.Add(new Tuple<Event, double>(eventItem, score));
            }

            // Sort by score in descending order and take the top N events
            var recommendedEvents = eventScores.OrderByDescending(e => e.Item2).Take(topN).Select(e => e.Item1).ToList();
            return recommendedEvents;
        }
    }


}
