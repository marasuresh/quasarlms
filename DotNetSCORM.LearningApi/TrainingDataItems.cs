using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.LearningComponents;
using Microsoft.LearningComponents.Storage;
using Schema = DotNetSCORM.LearningAPI.Schema;
using DotNetSCORM.LearningAPI.LearningComponentsHelper;


namespace DotNetSCORM.LearningAPI
{

    //Provides a data container used to populate the TrainingList GridView coontrol
    //TODO: As this control evolves into Catalog management this class will need to change
    public class TrainingDataItems
    {
        private PackageItemIdentifier _packageId;

        public PackageItemIdentifier PackageID
        {
            get { return _packageId; }
            set { _packageId = value; }
        }

        private string _packageFileName;

        public string PackageFileName
        {
            get { return _packageFileName; }
            set { _packageFileName = value; }
        }

        private ActivityPackageItemIdentifier _organizationId;

        public ActivityPackageItemIdentifier OrganizationId
        {
            get { return _organizationId; }
            set { _organizationId = value; }
        }


        private string _organizationTitle;

        public string OrganizationTitle
        {
            get { return _organizationTitle; }
            set { _organizationTitle = value; }
        }


        private AttemptItemIdentifier _attemptID;

        public AttemptItemIdentifier AttemptID
        {
            get { return _attemptID; }
            set { _attemptID = value; }
        }

        private DateTime _uploadTimeDate;

        public DateTime UploadTimeDate
        {
            get { return _uploadTimeDate; }
            set { _uploadTimeDate = value; }
        }

        private AttemptStatus _attemptStatus;

        public AttemptStatus AttemptStatus
        {
            get { return _attemptStatus; }
            set { _attemptStatus = value; }
        }

        private float _score;

        public float Score
        {
            get { return _score; }
            set { _score = value; }
        }

        // Strings created on the fly based on information set in other elements
        public string TrainingName
        {
            get
            {
                if (_organizationTitle.Length == 0)
                    return _packageFileName;
                else
                    return String.Format("{0} - {1}", _packageFileName, _organizationTitle);
            }
        }

        public string AttemptStatusString
        {
            get
            {
                if (_attemptStatus == null)
                    return "Not Started";
                else
                    return _attemptStatus.ToString();
            }
        }

        public string ScoreString
        {
            get
            {
                return String.Format("{0:0}%", Math.Round(_score));
            }

        }
        private bool _samePackage;

        public bool SamePackage
        {
            get { return _samePackage; }
            set { _samePackage = value; }
        }

    }

    public class TrainingDataItemCtrl
    {


        /// <summary>
        /// Reads a <c>DataTable</c>, returned by <c>Job.Execute</c>, containing the results requested
        /// by a previous call to <c>RequestMyTraining</c>.  Converts the results to HTML rows, added
        /// to a given HTML table.
        /// </summary>
        ///

        public List<TrainingDataItems> List(DataTable dataTable)
        {
            
            List<TrainingDataItems> LearningItems = new List<TrainingDataItems>();

             // loop once for each organization of each package
            PackageItemIdentifier previousPackageId = null;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                // extract information from <dataRow> into local variables
                TrainingDataItems Item = new TrainingDataItems();
                PackageItemIdentifier packageId;
                LStoreHelper.CastNonNull(dataRow[Schema.MyAttemptsAndPackages.PackageId],
                    out packageId);
                Item.PackageID = packageId;
                string packageFileName;
                LStoreHelper.CastNonNull(dataRow[Schema.MyAttemptsAndPackages.PackageFileName],
                    out packageFileName);
                Item.PackageFileName = packageFileName;
                ActivityPackageItemIdentifier organizationId;
                LStoreHelper.CastNonNull(dataRow[Schema.MyAttemptsAndPackages.OrganizationId],
                    out organizationId);
                Item.OrganizationId = organizationId;
                string organizationTitle;
                LStoreHelper.CastNonNull(dataRow[Schema.MyAttemptsAndPackages.OrganizationTitle],
                    out organizationTitle);
                Item.OrganizationTitle = organizationTitle;
                AttemptItemIdentifier attemptId;
                LStoreHelper.Cast(dataRow[Schema.MyAttemptsAndPackages.AttemptId],
                    out attemptId);
                Item.AttemptID = attemptId;
                DateTime? uploadDateTime;
                LStoreHelper.Cast(dataRow[Schema.MyAttemptsAndPackages.UploadDateTime],
                    out uploadDateTime);
                Item.UploadTimeDate = (DateTime)uploadDateTime;
                AttemptStatus? attemptStatus;
                LStoreHelper.Cast(dataRow[Schema.MyAttemptsAndPackages.AttemptStatus],
                    out attemptStatus);
                Item.AttemptStatus = attemptStatus ?? default(AttemptStatus);
                float? score;
                LStoreHelper.Cast(dataRow[Schema.MyAttemptsAndPackages.TotalPoints],
                    out score);
                if (score != null)
                    Item.Score = (float)score;

                // if this <dataRow> is another organization (basically another "table of contents")
                // within the same package as the previous <dataRow>, set <samePackage> to true
                bool samePackage = ((previousPackageId != null) &&
                    ( Item.PackageID.GetKey() == previousPackageId.GetKey()));
//TODO: Do we need SamePackage in the DataItems Class?

                LearningItems.Add(Item);
                previousPackageId = Item.PackageID;
            }
            return LearningItems;
        }

    }


}
