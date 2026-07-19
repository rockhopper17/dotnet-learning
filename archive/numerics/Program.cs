using MathNet.Numerics.LinearAlgebra;

// Define the input data
double[] x = { 1, 2, 3, 4, 5 };
double[] y = { 2.1, 3.9, 6.2, 8.1, 9.8 };

// Convert the input data to matrices
var xMatrix = Matrix<double>.Build.DenseOfColumnArrays(x);
var yMatrix = Matrix<double>.Build.DenseOfColumnArrays(y);

// Add a column of 1s to the x matrix
var ones = Vector<double>.Build.Dense(x.Length, 1);
var xWithOnes = xMatrix.InsertColumn(1, ones);

// Calculate the least squares solution
var beta = xWithOnes.QR().Solve(yMatrix);

// The gradient is the first element of the beta vector
double gradient = beta[0];

// Output the result
Console.WriteLine("The gradient is: " + gradient);

