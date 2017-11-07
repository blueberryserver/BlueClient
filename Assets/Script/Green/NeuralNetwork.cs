using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix
{
    public float[,] _data = null;

    static Matrix _empty = new Matrix(1, 1, 0);
    public static Matrix Empty()
    {
        return _empty;
    }

    public Matrix(int row, int col, float factor)
    {
        _data = new float[row, col];
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                _data[i, j] = factor * UnityEngine.Random.Range(0.0f, 1.0f);
            }
        }
    }

    public static Matrix Dot(Matrix a, Matrix b)
    {
        if (a._data.GetLength(1) != b._data.GetLength(0))
        {
            Debug.Log("Error dot matrix");
            return _empty;
        }

        int abRowCol = a._data.GetLength(1);

        int rRow = a._data.GetLength(0);
        int rCol = b._data.GetLength(1);
        Matrix r = new Matrix(rRow, rCol, 0.0f);

        for (int i = 0; i < rRow; i++)
        {
            for (int j = 0; j < rCol; j++)
            {
                float v = 0.0f;
                for (int k = 0; k < abRowCol; k++)
                {
                    v += a._data[i, k] * b._data[k, j];
                }

                r._data[i, j] = v;
            }
        }

        return r;
    }

    public static Matrix Sum(Matrix a, Matrix b)
    {
        if (a._data.GetLength(0) != b._data.GetLength(0) ||
            a._data.GetLength(1) != b._data.GetLength(1))
        {
            Debug.Log("Error sum matrix");
            return _empty;
        }

        int rRow = a._data.GetLength(0);
        int rCol = a._data.GetLength(1);
        Matrix r = new Matrix(rRow, rCol, 0.0f);

        for (int i = 0; i < rRow; i++)
        {
            for (int j = 0; j < rCol; j++)
            {
                r._data[i, j] = a._data[i, j] + b._data[i, j];
            }
        }

        return r;
    }

    public static float Max(Matrix a)
    {
        float max = 0.0f;
        int rRow = a._data.GetLength(0);
        int rCol = a._data.GetLength(1);

        for (int i = 0; i < rRow; i++)
        {
            for (int j = 0; j < rCol; j++)
            {
                if (max < a._data[i, j])
                {
                    max = a._data[i, j];
                }
            }
        }

        return max;
    }

    public static float SumElements(Matrix a)
    {
        float sum = 0.0f;
        int rRow = a._data.GetLength(0);
        int rCol = a._data.GetLength(1);

        for (int i = 0; i < rRow; i++)
        {
            for (int j = 0; j < rCol; j++)
            {
                sum += a._data[i, j];
            }
        }

        return sum;
    }

    public static Matrix Tanh(Matrix x)
    {
        int rRow = x._data.GetLength(0);
        int rCol = x._data.GetLength(1);
        Matrix r = new Matrix(rRow, rCol, 0.0f);

        for (int i = 0; i < rRow; i++)
        {
            for (int j = 0; j < rCol; j++)
            {
                r._data[i, j] = (float)System.Math.Tanh(x._data[i, j]);
            }
        }

        return r;
    }
}

public class NeuralNetworkLayer
{
    public virtual Matrix Forward(Matrix x)
    {
        return Matrix.Empty();
    }
    public virtual Matrix Backward(Matrix dout)
    {
        return Matrix.Empty();
    }
    public virtual void UpdateBackward()
    {
        
    }
    public virtual void Loss()
    {

    }
    public virtual Matrix Predict(Matrix x)
    {
        return Matrix.Empty();
    }
}

public class TanhLayer : NeuralNetworkLayer
{
    public Matrix _out;

    public override Matrix Forward(Matrix x)
    {
        Matrix r = Matrix.Tanh(x);
        _out = r;
        return _out;
    }
    public override Matrix Backward(Matrix dout)
    {
        int rRow = dout._data.GetLength(0);
        int rCol = dout._data.GetLength(1);
        Matrix r = new Matrix(rRow, rCol, 0.0f);

        for (int i = 0; i < rRow; i++)
        {
            for (int j = 0; j < rCol; j++)
            {
                
                r._data[i, j] = dout._data[i, j] * (1.0f - (float)System.Math.Pow(_out._data[i, j], 2.0f));
            }
        }

        return r;
    }
    public override void UpdateBackward()
    {

    }
    public override void Loss()
    {

    }
    public override Matrix Predict(Matrix x)
    {
        return Matrix.Empty();
    }
}

public class NeuralNetwork
{
    public Dictionary<string, Matrix> _params = new Dictionary<string, Matrix>();

    public NeuralNetwork(int inputSize, int hiddenSize, int outputSize, float weightInitStd=0.01f)
    {
        _params.Add("W1", new Matrix(inputSize, hiddenSize, weightInitStd));
        _params.Add("b1", new Matrix(1, hiddenSize, 0.0f));
        _params.Add("W2", new Matrix(hiddenSize, outputSize, weightInitStd));
        _params.Add("b2", new Matrix(1, outputSize, 0.0f));
    }

    public Matrix Sigmoid(Matrix xMat)
    {
        int rRow = xMat._data.GetLength(0);
        int rCol = xMat._data.GetLength(1);
        Matrix r = new Matrix(rRow, rCol, 0.0f);

        for (int i = 0; i < rRow; i++)
        {
            for (int j = 0; j < rCol; j++)
            {
                float x = xMat._data[i, j];
                r._data[i, j] = 1.0f / (1.0f + Mathf.Exp(-x)); 
            }
        }

        return r;
    }

    public Matrix Softmax(Matrix xMat)
    {
        int rRow = xMat._data.GetLength(0);
        int rCol = xMat._data.GetLength(1);
        Matrix r = new Matrix(rRow, rCol, 0.0f);

        float max = Matrix.Max(xMat);
        Matrix exp_a = new Matrix(rRow, rCol, 0.0f);

        for (int i = 0; i < rRow; i++)
        {
            for (int j = 0; j < rCol; j++)
            {
                float x = xMat._data[i, j];

                exp_a._data[i, j] = Mathf.Exp(x - max);
            }
        }

        float sum_exp_a = Matrix.SumElements(exp_a);

        for (int i = 0; i < rRow; i++)
        {
            for (int j = 0; j < rCol; j++)
            {
                r._data[i, j] = exp_a._data[i, j] / sum_exp_a;
            }
        }

        return r;
    }

    public Vector3 Predict(float[] x)
    {
        Matrix xMat = new Matrix(1, x.Length, 0.0f);

        for (int i = 0; i < x.Length; i++)
        {
            xMat._data[0, i] = x[i];
        }

        Matrix W1 = _params["W1"];
        Matrix b1 = _params["b1"];
        Matrix W2 = _params["W2"];
        Matrix b2 = _params["b2"];

        Matrix a1 = Matrix.Sum(Matrix.Dot(xMat, W1), b1);
        Matrix z1 = Sigmoid(a1);
        Matrix a2 = Matrix.Sum(Matrix.Dot(z1, W2), b2);
        Matrix yMat = a2;// Softmax(a2);
        //a1 = np.dot(x, W1) + b1
        //z1 = Sigmoid(a1)
        //a2 = np.dot(z1, W2) + b2
        //y = Softmax(a2)
        //return y
        Vector3 y = new Vector3(yMat._data[0, 0], 0.0f/*yMat._data[0, 1]*/, yMat._data[0, 2]);
        return y;
    }
}
