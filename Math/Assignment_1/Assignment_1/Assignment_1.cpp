/****************************************************************************
Project:		Assignment_1.exe

Student Names:	Jordan Thompson

File Name:		Assignment_1.cpp

Date:			September 22, 2015

Description:	The purpose of this class is to represent the View, which
handles the initial drawing of the bitmap images as well as
the movement of the reptile.
*****************************************************************************/

/***********************************************
******* INCLUDES *******************************
************************************************/
#include "stdafx.h"
#include "math.h"
#include <iostream>
#include <string>

/***********************************************
******* NAMESPACE ******************************
************************************************/
using namespace std;


/************************************************************************************
*  FUNCTION :		calculateSin
*
*  DESCRIPTION :	Based on a given 'x' value and a given 'N' value, this function
*					calculates sin(x).
*************************************************************************************/
double calculateSin(double x, double N)
{
	double sum = 0.00;	//Sum of all terms
	double term = 0.00; //Sum of indiviual term

	//Loop until n reaches N (the limit)
	for (int n = 0; n <= N; n++)
	{
		term = 1.00; //resets the term value

		//Calculating each term
		for (int i = 1; i <= (2 * n + 1); i++)
		{
			term *= x / i;
		}

		//Adding the calculated term to the total sum
		if (n % 2 == 0)
		{
			sum += term;
		}
		else
		{
			sum -= term;
		}
	}
	return sum;
}

/************************************************************************************
*  FUNCTION :		calculateAccuracy
*
*  DESCRIPTION :	Based on a given 'x' value and a given accuracy value, this function
*					calculates sin(x) to the required accuracy.
*************************************************************************************/
double calculateAccuracy(double x, double accuracy)
{
	int N = 1;
	double currentDifference = 0.0;
	double currentValue = 0.00;
	double nextValue = 0.00;
	
	while(true)
	{
		currentValue = calculateSin(x, N);	//Calculating the sin(x) for N
		nextValue = calculateSin(x, N + 1); //Calculating the sin(x) for N + 1

		currentDifference = abs(currentValue - nextValue); //Caluclating the difference

		if (accuracy > currentDifference) //Comparing the difference to the required accuracy
		{
			break;
		}
		N += 1;
	}

	return currentValue;

}

int main()
{
	double x = 0.0;
	double N = 0.0;
	double accuracy = 0.00;
	int choice = 0;

	cout.precision(17);
	cout << "1. Calculate sin(x)" << endl;
	cout << "2. Calculate Accuracy" << endl;
	cout << "3. Print out Assignment" << endl;
	cout << "4. Exit" << endl;
	cin >> choice;

	//Caluclates sin(x) for a given N
	if (choice == 1)
	{
		cout << "Enter the 'x' value: ";
		cin >> x;
		cout << "Enter the 'N' value: ";
		cin >> N;		
		cout << "Your answer is: " << calculateSin(x, N) << endl;		
	}
	//Caluclates sin(x) for a given accuracy
	else if (choice == 2)
	{
		cout << "Enter the 'x' value: ";
		cin >> x;
		cout << "Enter a required accuracy value [e.g. 0.001 for 10^-3]: ";
		cin >> accuracy;		
		cout << "Your answer is: " << calculateAccuracy(x, accuracy) << endl;		
	}
	//Prints out the assignment based on given values
	else if (choice == 3)
	{
		//Part 1
		cout << "Part 1:" << endl;
		cout << "x=0.1 N=10 sin(x)=" << calculateSin(0.1,10) << endl;
		cout << "x=0.1 N=10 sin(x)=" << calculateSin(0.1, 11) << endl;
		cout << "x=0.1 N=10 sin(x)=" << calculateSin(0.1, 20) << endl;
		cout << "**************************************" << endl;
		cout << "x=3.0 N=10 sin(x)=" << calculateSin(3.0, 10) << endl;
		cout << "x=3.0 N=10 sin(x)=" << calculateSin(3.0, 11) << endl;
		cout << "x=3.0 N=10 sin(x)=" << calculateSin(3.0, 20) << endl;

		//Part 2
		cout << "Part 2:" << endl;
		cout << "x=0.1 Accuracy=0.001    sin(x)=" << calculateAccuracy(0.1, 0.001) << endl;
		cout << "x=0.1 Accuracy=0.000001 sin(x)=" << calculateAccuracy(0.1, 0.000001) << endl;
	    cout << "**************************************" << endl;
		cout << "x=3.0 Accuracy=0.001    sin(x)=" << calculateAccuracy(3.0, 0.001) << endl;
		cout << "x=3.0 Accuracy=0.000001 sin(x)=" << calculateAccuracy(3.0, 0.000001) << endl;
	}
	else if (choice == 4)
	{
		exit(0);
	}

	cout << endl;
	main();

	return 0;
}
