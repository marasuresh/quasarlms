using System;
using System.Data;
using System.Data.SqlClient;

namespace DCEAccessLib
{
    public enum TestType
    {
        test = 1,
        practice = 2,
        // all questionnaire x%3=0
        questionnaire = 3,
        globalquestionnaire = 6
    }

}