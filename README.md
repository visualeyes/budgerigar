# Budgerigar 
[![Build status](https://ci.appveyor.com/api/projects/status/github/visualeyes/budgerigar?branch=master&svg=true)](https://ci.appveyor.com/project/visualeyes-builder/budgerigar/branch/master) 
[![Coverage Status](https://coveralls.io/repos/visualeyes/budgerigar/badge.svg?branch=master&service=github)](https://coveralls.io/github/visualeyes/budgerigar?branch=master)
[![Budgerigar Nuget Version](https://img.shields.io/nuget/v/budgerigar.svg)](https://www.nuget.org/packages/Budgerigar/)
[![Budgerigar Nuget Downloads](https://img.shields.io/nuget/dt/budgerigar.svg)](https://www.nuget.org/packages/Budgerigar/)

## What is Budgerigar?
Budgerigar is a Performance Budgetting tool for .NET. 
It allows you to set performance targets and take action if those targets are not met.

For more info you can read the [blog post](https://medium.com/@johncmckim/performance-budgeting-net-8624604719dc).



## Getting Started with Budgerigar

**Getting Budgerigar**

Install from Nuget: `Install-Pacakge Budgerigar`

**Using Budgerigar**

    
    var budgetter = new PerformanceBudgetter();
    var response = await budgetter.RunWithBudgetAsync("some-task-name", 1.0M, async (budget) => { 
        // do work  
        var lotsOfData = await budget.StepAsync("get-data", async () => {
            return await provider.GetLotsOfDataAsync();
        });
    
        await budget.StepAsync("proccess-data", async () => await processor.PorcessDataAsync(lotsOfData));
    }, (result) => {
        if(result.IsOver) {
            logger.Error(result.GetDetailedOutput());
        }
    });

