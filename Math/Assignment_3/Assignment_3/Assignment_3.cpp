/****************************************************************************
Project:		Assignment_3

Student Name:	Jordan Thompson

File Name:		Assignment_3.cpp

Date:			October 14, 2015

Description:	This class solves a system of linear equations using the
				Guass-Jordan Elimination method.
*****************************************************************************/

/***********************************************
******* INCLUDES *******************************
************************************************/
#include "stdafx.h"
#include "math.h"
#include <iostream>
#include <string>
#include <fstream>
#include <math.h>

/***********************************************
******* NAMESPACE ******************************
************************************************/
using namespace std;

int main(int argc, char* argv[])
{	
	float matrixA[10][10];
	float matrixB[10];
	int N = 0;

	float tempElement = 0.0f;
	int count = 0;
	//float swapEl = 0.0f;

	string data;
	fstream file;

	file.open("gauss.txt");

	if (file.is_open())
	{
		//Gets the 'N' value from the file
		getline(file, data);
		N = atoi(data.c_str());

		cout << "*****************************" << endl;

		//Prints out MatrixA to the screen
		cout << "Matrix A: " << endl;
		for (int i = 0; i < N; i++)
		{
			for (int j = 0; j < N; j++)
			{
				file >> matrixA[i][j];
				cout << matrixA[i][j] << " ";
			}
			cout << endl;
		}
		cout << endl;

		//Prints out MatrixB to the screen
		cout << "Matrix B: " << endl;
		for (int i = 0; i < N; i++)
		{
			file >> matrixB[i];
			cout << matrixB[i] << " " << endl;
		}

		file.close();

		//Guass-Jordan Elimination
		cout << "*****************************" << endl;
		cout << "Guass-Jordan Elimination:" << endl;
		cout << "*****************************" << endl;		
		
		/*
		The following code allows for row swapping for the matrix.
		It is currently commented out since this particular assignment
		and sample input do not require it. But it would be useful to have
		
		// Swap rows in the matrix if needed
		for (int j = 0; j < N; j++) //Loops through each column
		{
			for (int i = j; i < N + 1; i++) //Loops through each row
			{
				if (i == j && matrixA[i][j] == 0)
				{
					//Checks each row for non zero value
					// then switches the rows when a non zero value is found
					for (int k = i + 1; k < N + 1; k++) 
					{
						if (k >= N)
						{
							k = 0;
						}

						if (matrixA[k][j] != 0)
						{
							for (int r = 0; r < N; r++)
							{
								swapEl = matrixA[i][r];
								matrixA[i][r] = matrixA[k][r];
								matrixA[k][r] = swapEl;
							}

							swapEl = matrixB[i];
							matrixB[i] = matrixB[k];
							matrixB[k] = swapEl;

							break;
						}

						count++;

						if (count == N)
						{
							break;
						}
					}
				}
			}
		}
		*/	
		
		for (int j = 0; j < N; j++) //Loops through each column
		{
			for (int i = j; i < N + 1; i++) //Loops through each row
			{
				tempElement = matrixA[i][j];

				for (int k = j; k < N; k++) //Loops through to check each row
				{
					//When an element needs to be changed to '1'
					if (i == j)
					{
						matrixA[i][k] = matrixA[i][k] / tempElement;
					}
					//When an element needs to be changed to '0'		
					else
					{
						matrixA[i][k] = matrixA[i][k] - (matrixA[j][k] * tempElement);
					}
				}

				//When an element needed to be changed to '1'
				if (i == j)
				{
					matrixB[i] = matrixB[i] / tempElement;
				}
				//When an element needed to be changed to '0'
				else
				{
					matrixB[i] = matrixB[i] - (matrixB[j] * tempElement);
				}

				//Keeps track of how many elements have been updated
				count++;

				//If all elements have been updated, break out of loop
				if (count == N)
				{
					break;
				}

				//If the max row has been reached, then go back to the first row
				if (i == (N - 1))
				{
					i = -1;
				}
			}
			count = 0;
		}

		//Prints out MatrixC (the result from the Matrix Multiplication)
		cout << "Matrix X: " << endl;
		for (int i = 0; i < N; i++)
		{
			cout << "X" << i + 1 << ": " << matrixB[i] << " ";
			cout << endl;
		}
		cout << "*****************************" << endl;
	}
	else
	{
		cout << "Unable to open file" << endl;
		cout << endl;
	}

	return 0;
}
