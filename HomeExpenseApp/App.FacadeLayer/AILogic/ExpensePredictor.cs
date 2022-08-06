using App.Entities.Core;
using App.Interfaces.AIEngine;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using App.Interfaces.Repository;

namespace App.FacadeLayer.AILogic
{
    public class ExpensePredictor : IExpensePredictor
    {
        private readonly MLContext mLContext;
     
        public ExpensePredictor(MLContext mLContext)
        {
            //Step 1: Create ML context
            this.mLContext = mLContext;
        }

        public string PredictValue(int weekNumber)
        {
            throw new NotImplementedException();
        }

        public bool TrainModel(IEnumerable<ExpenseEntry> expenseEntries)
        {
            var totalDataCount = expenseEntries.Count();
            var trainingDataCount = Convert.ToInt32(totalDataCount * 0.7);
            var trainingData = expenseEntries.Take(trainingDataCount);
            var testingData = expenseEntries.Skip(trainingDataCount);

            bool isSuccess = false;
            //Step 2: Load data
            var dataSourceTraining = mLContext.Data.LoadFromEnumerable(trainingData);
            var dataSourceTesting = mLContext.Data.LoadFromEnumerable(testingData);
            //Step 3: Transform data - NA
            //Step 4: Choose Algorithm
            IEstimator<ITransformer> trainingPipeline = mLContext.Regression.Trainers.Sdca(labelColumnName: "Amount", featureColumnName: "NotedMonth,NotedYear");
            //Step 5 : Train Model
            var model = trainingPipeline.Fit(dataSourceTraining);
            //Step 6: Evaluate Model
            IDataView predictions = model.Transform(dataSourceTesting);
            var metrics = mLContext.Regression
                .Evaluate(predictions, labelColumnName: "Amount");
            //Step 7: Deploy Model - pending

            return isSuccess;
            
        }
    }
}
