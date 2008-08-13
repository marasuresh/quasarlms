using System;
using System.Data;
using System.Data.SqlClient;

namespace DCEAccessLib
{
    public enum EntityType
    {
        // note, these values is used in database with different triggers
        // do not modify them ever!
        student = 1,
        user = 2,
        content = 3,
        questionnaire = 4,
        test = 5,
        course = 6,
        courserequest = 7,
        training = 9,
        group = 10,
        theme = 11,
        task = 12,
        track = 13,
        coursedomain = 14,
        globalquestionnaire = 15
    }
}