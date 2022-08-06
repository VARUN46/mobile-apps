using App.Entities.Core;
using App.FacadeLayer.AILogic;
using App.Interfaces.AIEngine;

namespace App.UnitTests
{
    public class ExpensePredictionTests
    {
        [Fact]
        public void ModelTrainingTest()
        {
            var list = new List<ExpenseEntry>
            {
                new ExpenseEntry{ Amount = 200 , NotedMonth = 1,NotedYear=2019},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 2,NotedYear=2019},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 3,NotedYear=2019},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 4,NotedYear=2019},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 5,NotedYear=2019},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 6,NotedYear=2019},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 7,NotedYear=2019},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 8,NotedYear=2019},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 9,NotedYear=2019},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 10,NotedYear=2019},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 11,NotedYear=2019},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 12,NotedYear=2019},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 1,NotedYear=2020},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 2,NotedYear=2020},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 3,NotedYear=2020},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 4,NotedYear=2020},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 5,NotedYear=2020},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 6,NotedYear=2020},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 7,NotedYear=2020},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 8,NotedYear=2020},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 9,NotedYear=2020},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 10,NotedYear=2020},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 11,NotedYear=2020},
                new ExpenseEntry{ Amount = 200 , NotedMonth = 12,NotedYear=2020},
            };
            IExpensePredictor predictor = new ExpensePredictor(new Microsoft.ML.MLContext());
            bool isTrained = predictor.TrainModel(list);
            Assert.True(isTrained);
        }
    }
}