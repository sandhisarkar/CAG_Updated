using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DataLayerDefs
{
    public enum Mode
    {
        _Edit, _Add, _Delete, _View, _Rename
    };
    
        public struct PersonDetails
        {
            private bool _selected;
            private string _district_code;
            private string _RO_code;
            private string _Book;
            private string _Deed_year;
            private string _Deed_no;
            private string _serial;
            private string _initial_name;
            private string _First_name;
            private string _Last_Name;
            private string _Proffession;
            private string _Cast;
            private string _Proffession_Name;
            private string _Cast_Name;
            private string _Status_code;
            private string _Status_name;
            private string _EX_CL;
            private string _Admit_code;
            private string _Address;
            private string _Address_district_code;
            private string _Address_district_name;
            private string _Address_ps_code;
            private string _Address_ps_Name;
            private string _City_Name;
            private string _PIN;
            private string _Father_mother;
            private string _Rel_code;
            private string _Rel;
            private string _Relation;
            private string _Dummy_Relation;
            private string _road_code;
            private string _Road;
            private string _F_initial_name;
            private string _F_First_name;
            private string _F_Last_Name;
            private string _more;
            private string _other_party_code;
            private string _linked_to;
            private string _created_by;
            private string _created_dttm;
            //private string _exception;

            public string Serial
            {
                get { return _serial; }
                set
                {
                    _serial = value;
                }
            }
            public string Status_code
            {
                get { return _Status_code; }
                set
                {
                    _Status_code = value;
                }
            }
            public string initial_name
            {
                get { return _initial_name; }
                set
                {
                    _initial_name = value;
                }
            }
            public string First_name
            {
                get { return _First_name; }
                set
                {
                    _First_name = value;
                }
            }
            public string Last_Name
            {
                get { return _Last_Name; }
                set
                {
                    _Last_Name = value;
                }
            }
            public string Address
            {
                get { return _Address; }
                set
                {
                    _Address = value;
                }
            }
            public string Address_district_name
            {
                get { return _Address_district_name; }
                set
                {
                    _Address_district_name = value;
                }
            }
            public string Address_ps_Name
            {
                get { return _Address_ps_Name; }
                set
                {
                    _Address_ps_Name = value;
                }
            }
            public string Relation
            {
                get { return _Relation; }
                set
                {
                    _Relation = value;
                }
            }

            public bool Selected
            {
                get { return _selected; }
                set
                {
                    _selected = value;
                    //this.NotifyPropertyChanged("Selected");
                }
            }
            public string linked_to
            {
                get { return _linked_to; }
                set
                {
                    _linked_to = value;
                }
            }
            public string other_party_code
            {
                get { return _other_party_code; }
                set
                {
                    _other_party_code = value;
                }
            }
            public string district_code
            {
                get { return _district_code; }
                set
                {
                    _district_code = value;
                }
            }
            public string more
            {
                get { return _more; }
                set
                {
                    _more = value;
                }
            }
            public string RO_code
            {
                get { return _RO_code; }
                set
                {
                    _RO_code = value;
                }
            }
            public string Book
            {
                get { return _Book; }
                set
                {
                    _Book = value;
                }
            }
            public string Deed_year
            {
                get { return _Deed_year; }
                set
                {
                    _Deed_year = value;
                }
            }
            public string Deed_no
            {
                get { return _Deed_no; }
                set
                {
                    _Deed_no = value;
                }
            }


            public string Admit_code
            {
                get { return _Admit_code; }
                set
                {
                    _Admit_code = value;
                }
            }

            public string F_Initial_name
            {
                get { return _F_initial_name; }
                set
                {
                    _F_initial_name = value;
                }
            }
            public string F_First_name
            {
                get { return _F_First_name; }
                set
                {
                    _F_First_name = value;
                }
            }
            public string F_Last_Name
            {
                get { return _F_Last_Name; }
                set
                {
                    _F_Last_Name = value;
                }
            }
            public string Proffession_Name
            {
                get { return _Proffession_Name; }
                set
                {
                    _Proffession_Name = value;
                }
            }
            public string Cast_Name
            {
                get { return _Cast_Name; }
                set
                {
                    _Cast_Name = value;
                }
            }
            public string Proffession
            {
                get { return _Proffession; }
                set
                {
                    _Proffession = value;
                }
            }
            public string Cast
            {
                get { return _Cast; }
                set
                {
                    _Cast = value;
                }
            }

            public string Status_name
            {
                get { return _Status_name; }
                set
                {
                    _Status_name = value;
                }
            }
            public string EX_CL
            {
                get { return _EX_CL; }
                set
                {
                    _EX_CL = value;
                }
            }

            public string Address_district_code
            {
                get { return _Address_district_code; }
                set
                {
                    _Address_district_code = value;
                }
            }

            public string Address_ps_code
            {
                get { return _Address_ps_code; }
                set
                {
                    _Address_ps_code = value;
                }
            }

            public string City_Name
            {
                get { return _City_Name; }
                set
                {
                    _City_Name = value;
                }
            }
            public string PIN
            {
                get { return _PIN; }
                set
                {
                    _PIN = value;
                }
            }
            public string Father_mother
            {
                get { return _Father_mother; }
                set
                {
                    _Father_mother = value;
                }
            }
            public string Rel_code
            {
                get { return _Rel_code; }
                set
                {
                    _Rel_code = value;
                }
            }
            public string Rel
            {
                get { return _Rel; }
                set
                {
                    _Rel = value;
                }
            }

            public string Relation_Dummy
            {
                get { return _Dummy_Relation; }
                set
                {
                    _Dummy_Relation = value;
                }
            }
            public string road_code
            {
                get { return _road_code; }
                set
                {
                    _road_code = value;
                }
            }
            public string Road
            {
                get { return _Road; }
                set
                {
                    _Road = value;
                }
            }
            public string Created_By
            {
                get { return _created_by; }
                set { _created_by = value; }
            }
            public string Created_Dttm
            {
                get { return _created_dttm; }
                set { _created_dttm = value; }
            }
            
        }
        public struct PersonDetailsException
        {
            private string _district_code;
            private string _RO_code;
            private string _Book;
            private string _Deed_year;
            private string _Deed_no;
            private string _item_no;
            private string _serial;
            private string _exception;
            private string _excDetails;

            public string district_code
            {
                get { return _district_code; }
                set
                {
                    _district_code = value;
                }
            }
            public string RO_code
            {
                get { return _RO_code; }
                set
                {
                    _RO_code = value;
                }
            }
            public string Book
            {
                get { return _Book; }
                set
                {
                    _Book = value;
                }
            }
            public string Deed_year
            {
                get { return _Deed_year; }
                set
                {
                    _Deed_year = value;
                }
            }
            public string Deed_no
            {
                get { return _Deed_no; }
                set
                {
                    _Deed_no = value;
                }
            }
            public string item_no
            {
                get { return _item_no; }
                set
                {
                    _item_no = value;
                }
            }
            public string serial
            {
                get { return _serial; }
                set
                {
                    _serial = value;
                }
            }
            public string exception
            {
                get { return _exception; }
                set
                {
                    _exception = value;
                }
            }

            public string excDetails
            {
                get { return _excDetails; }
                set
                {
                    _excDetails = value;
                }
            }
        }
    public struct PropertyDetailsException
        {
            private string _district_code;
            private string _RO_code;
            private string _Book;
            private string _Deed_year;
            private string _Deed_no;
            private string _item_no;
            private string _serial;
            private string _exception;
            private string _excDetails;

            public string district_code
            {
                get { return _district_code; }
                set
                {
                    _district_code = value;
                }
            }
            public string RO_code
            {
                get { return _RO_code; }
                set
                {
                    _RO_code = value;
                }
            }
            public string Book
            {
                get { return _Book; }
                set
                {
                    _Book = value;
                }
            }
            public string Deed_year
            {
                get { return _Deed_year; }
                set
                {
                    _Deed_year = value;
                }
            }
            public string Deed_no
            {
                get { return _Deed_no; }
                set
                {
                    _Deed_no = value;
                }
            }
            public string item_no
            {
                get { return _item_no; }
                set
                {
                    _item_no = value;
                }
            }
            public string serial
            {
                get { return _serial; }
                set
                {
                    _serial = value;
                }
            }
            public string exception
            {
                get { return _exception; }
                set
                {
                    _exception = value;
                }
            }
            public string excDetails
            {
                get { return _excDetails; }
                set
                {
                    _excDetails = value;
                }
            }
        }
    public struct outSideWBList
    {
        private string _district_code;
        private string _RO_code;
        private string _Book;
        private string _Deed_year;
        private string _Deed_no;
        private string _serial;
        private string _exception;

        public string district_code
        {
            get { return _district_code; }
            set
            {
                _district_code = value;
            }
        }
        public string RO_code
        {
            get { return _RO_code; }
            set
            {
                _RO_code = value;
            }
        }
        public string Book
        {
            get { return _Book; }
            set
            {
                _Book = value;
            }
        }
        public string Deed_year
        {
            get { return _Deed_year; }
            set
            {
                _Deed_year = value;
            }
        }
        public string Deed_no
        {
            get { return _Deed_no; }
            set
            {
                _Deed_no = value;
            }
        }

        public string serial
        {
            get { return _serial; }
            set
            {
                _serial = value;
            }
        }
        public string exception
        {
            get { return _exception; }
            set
            {
                _exception = value;
            }
        }
        
    }
        public struct deedDetailsException
        {
            private string _district_code;
            private string _RO_code;
            private string _Book;
            private string _Deed_year;
            private string _Deed_no;
            private string _serial;
            private string _exception;
            private string _excDetails;

            public string district_code
            {
                get { return _district_code; }
                set
                {
                    _district_code = value;
                }
            }
            public string RO_code
            {
                get { return _RO_code; }
                set
                {
                    _RO_code = value;
                }
            }
            public string Book
            {
                get { return _Book; }
                set
                {
                    _Book = value;
                }
            }
            public string Deed_year
            {
                get { return _Deed_year; }
                set
                {
                    _Deed_year = value;
                }
            }
            public string Deed_no
            {
                get { return _Deed_no; }
                set
                {
                    _Deed_no = value;
                }
            }
            
            public string serial
            {
                get { return _serial; }
                set
                {
                    _serial = value;
                }
            }
            public string exception
            {
                get { return _exception; }
                set
                {
                    _exception = value;
                }
            }
            public string excDetails
            {
                get { return _excDetails; }
                set
                {
                    _excDetails = value;
                }
            }
        }
        public struct PropertyDetails
        {
            private bool _selected;
            private string _district_code;
            private string _RO_code;
            private string _Book;
            private string _Deed_year;
            private string _Deed_no;
            private string _serial;
            private string _Property_district_code;
            private string _Property_ro_code;
            private string _ps_code;
            private string _moucode;
            private string _Area_type;
            private string _GP_Muni_Corp_Code;
            private string _Ward;
            private string _Holding;
            private string _Premises;
            private string _road_code;
            private string _Plot_code_type;
            private string _Road;
            private string _Plot_No;
            private string _Bata_No;
            private string _Khatian_type;
            private string _khatian_No;
            private string _bata_khatian_no;
            private string _property_type;
            private string _Land_Area_acre;
            private string _Land_Area_bigha;
            private string _Land_Area_decimal;
            private string _Land_Area_katha;
            private string _Land_Area_chatak;
            private string _Land_Area_sqfeet;
            private string _Structure_area_in_sqFeet;
            private string _Property_details;
            private string _Police_Station;
            private string _District;
            private string _Where_Registered;
            private string _Nature_of_Transaction;
            private string _Ref_PS;
            private string _ref_mou;
            private string _JL_no;
            private string _other_Khatian;
            private string _other_plots;
            private string _land_type;
            private string _Ref_JL_Number;
            private string _created_by;
            private string _created_dttm;
            //private string _exception;
            //public event PropertyChangedEventHandler PropertyChanged;

            public string Serial
            {
                get { return _serial; }
                set
                {
                    _serial = value;
                }
            }
            public string district_code
            {
                get { return _district_code; }
                set
                {
                    _district_code = value;
                }
            }
            public string RO_code
            {
                get { return _RO_code; }
                set
                {
                    _RO_code = value;
                }
            }
            public string Book
            {
                get { return _Book; }
                set
                {
                    _Book = value;
                }
            }
            public string Deed_year
            {
                get { return _Deed_year; }
                set
                {
                    _Deed_year = value;
                }
            }
            public string Deed_no
            {
                get { return _Deed_no; }
                set
                {
                    _Deed_no = value;
                }
            }

            public string Property_district_code
            {
                get { return _Property_district_code; }
                set
                {
                    _Property_district_code = value;
                }
            }
            public string Property_ro_code
            {
                get { return _Property_ro_code; }
                set
                {
                    _Property_ro_code = value;
                }
            }
            public string ps_code
            {
                get { return _ps_code; }
                set
                {
                    _ps_code = value;
                }
            }
            public string moucode
            {
                get { return _moucode; }
                set
                {
                    _moucode = value;
                }

            }
            public string Area_type
            {
                get { return _Area_type; }
                set
                {
                    _Area_type = value;
                }
            }
            public string GP_Muni_Corp_Code
            {
                get { return _GP_Muni_Corp_Code; }
                set
                {
                    _GP_Muni_Corp_Code = value;
                }
            }
            public string Ward
            {
                get { return _Ward; }
                set
                {
                    _Ward = value;
                }
            }

            public string Ref_JL_Number
            {
                get { return _Ref_JL_Number; }
                set
                {
                    _Ref_JL_Number = value;
                }
            }
            public string land_type
            {
                get { return _land_type; }
                set
                {
                    _land_type = value;
                }
            }
            public string other_Khatian
            {
                get { return _other_Khatian; }
                set
                {
                    _other_Khatian = value;
                }
            }
            public string other_plots
            {
                get { return _other_plots; }
                set
                {
                    _other_plots = value;
                }
            }
            public string Ref_ps
            {
                get { return _Ref_PS; }
                set
                {
                    _Ref_PS = value;
                }
            }
            public string Ref_mou
            {
                get { return _ref_mou; }
                set
                {
                    _ref_mou = value;
                }
            }


            public string Holding
            {
                get { return _Holding; }
                set
                {
                    _Holding = value;
                }
            }
            public string Premises
            {
                get { return _Premises; }
                set
                {
                    _Premises = value;
                }
            }
            public string road_code
            {
                get { return _road_code; }
                set
                {
                    _road_code = value;
                }
            }
            public string Plot_code_type
            {
                get { return _Plot_code_type; }
                set
                {
                    _Plot_code_type = value;
                }
            }
            public string Road
            {
                get { return _Road; }
                set
                {
                    _Road = value;
                }
            }
            public string Plot_No
            {
                get { return _Plot_No; }
                set
                {
                    _Plot_No = value;
                }
            }
            public string Bata_No
            {
                get { return _Bata_No; }
                set
                {
                    _Bata_No = value;
                }
            }
            public string Khatian_type
            {
                get { return _Khatian_type; }
                set
                {
                    _Khatian_type = value;
                }
            }
            public string khatian_No
            {
                get { return _khatian_No; }
                set
                {
                    _khatian_No = value;
                }
            }
            public string bata_khatian_no
            {
                get { return _bata_khatian_no; }
                set
                {
                    _bata_khatian_no = value;
                }

            }
            public string property_type
            {
                get { return _property_type; }
                set
                {
                    _property_type = value;
                }
            }
            public string Land_Area_acre
            {
                get { return _Land_Area_acre; }
                set
                {
                    _Land_Area_acre = value;
                }
            }
            public string Land_Area_bigha
            {
                get { return _Land_Area_bigha; }
                set
                {
                    _Land_Area_bigha = value;
                }
            }
            public string Land_Area_decimal
            {
                get { return _Land_Area_decimal; }
                set
                {
                    _Land_Area_decimal = value;
                }
            }
            public string Land_Area_katha
            {
                get { return _Land_Area_katha; }
                set
                {
                    _Land_Area_katha = value;
                }
            }
            public string Land_Area_chatak
            {
                get { return _Land_Area_chatak; }
                set
                {
                    _Land_Area_chatak = value;
                }
            }
            public bool Selected
            {
                get { return _selected; }
                set
                {
                    _selected = value;
                    //this.NotifyPropertyChanged("Selected");
                }
            }
            public string Land_Area_sqfeet
            {
                get { return _Land_Area_sqfeet; }
                set
                {
                    _Land_Area_sqfeet = value;
                }
            }
            public string Structure_area_in_sqFeet
            {
                get { return _Structure_area_in_sqFeet; }
                set
                {
                    _Structure_area_in_sqFeet = value;
                }
            }
            public string Property_details
            {
                get { return _Property_details; }
                set
                {
                    _Property_details = value;
                }
            }
            public string Police_Station
            {
                get { return _Police_Station; }
                set
                {
                    _Police_Station = value;
                }
            }
            public string District
            {
                get { return _District; }
                set
                {
                    _District = value;
                }
            }
            public string Where_Registered
            {
                get { return _Where_Registered; }
                set
                {
                    _Where_Registered = value;
                }
            }
            public string Nature_of_Transaction
            {
                get { return _Nature_of_Transaction; }
                set
                {
                    _Nature_of_Transaction = value;
                }
            }
            public string JL_NO
            {
                get { return _JL_no; }
                set
                {
                    _JL_no = value;
                }
            }
            public string Created_By
            {
                get { return _created_by; }
                set { _created_by = value; }
            }
            public string Created_Dttm
            {
                get { return _created_dttm; }
                set { _created_dttm = value; }
            }
            
        }


        public struct PropertyDetailsWB
        {
            private bool _selected;
            private string _district_code;
            private string _RO_code;
            private string _Book;
            private string _Deed_year;
            private string _Deed_no;
            private string _serial;
            private string _Property_country_code;
            private string _Property_state_code;
            private string _Property_district_code;
            private string _thana;
            private string _mouza;
            private string _Plot_code_type;
            private string _Plot_No;
            private string _Khatian_type;
            private string _khatian_No;
            private string _land_use;
            //private string _Area;
            private string _property_type;
            private string _local_body_type;
            private string _other_details;
            private string _created_by;
            private string _created_dttm;
            private string _area_acre;
            private string _area_bigha;
            private string _area_decimal;
            private string _area_katha;
            private string _area_chatak;
            private string _area_sqtL;
            private string _total_decimal;
            private string _structure_sql;
            //private string _exception;
            //public event PropertyChangedEventHandler PropertyChanged;

            public string Serial
            {
                get { return _serial; }
                set
                {
                    _serial = value;
                }
            }
            public string district_code
            {
                get { return _district_code; }
                set
                {
                    _district_code = value;
                }
            }
            public string RO_code
            {
                get { return _RO_code; }
                set
                {
                    _RO_code = value;
                }
            }
            public string Book
            {
                get { return _Book; }
                set
                {
                    _Book = value;
                }
            }
            public string Deed_year
            {
                get { return _Deed_year; }
                set
                {
                    _Deed_year = value;
                }
            }
            public string Deed_no
            {
                get { return _Deed_no; }
                set
                {
                    _Deed_no = value;
                }
            }

           
            public string Property_country_code
            {
                get { return _Property_country_code; }
                set
                {
                    _Property_country_code = value;
                }
            }
            public string Property_state_code
            {
                get { return _Property_state_code; }
                set
                {
                    _Property_state_code = value;
                }
            }
            public string Property_district_code
            {
                get { return _Property_district_code; }
                set
                {
                    _Property_district_code = value;
                }
            }
            public string thana
            {
                get { return _thana; }
                set
                {
                    _thana = value;
                }
            }
            public string mouza
            {
                get { return _mouza; }
                set
                {
                    _mouza = value;
                }

            }
            
            
            public string Plot_code_type
            {
                get { return _Plot_code_type; }
                set
                {
                    _Plot_code_type = value;
                }
            }
            
            public string Plot_No
            {
                get { return _Plot_No; }
                set
                {
                    _Plot_No = value;
                }
            }
            
            public string Khatian_type
            {
                get { return _Khatian_type; }
                set
                {
                    _Khatian_type = value;
                }
            }
            public string khatian_No
            {
                get { return _khatian_No; }
                set
                {
                    _khatian_No = value;
                }
            }

            public string land_use
            {
                get { return _land_use; }
                set
                {
                    _land_use = value;
                }
            }
            //public string Area
            //{
            //    get { return _Area; }
            //    set
            //    {
            //        _Area = value;
            //    }
            //}
            public string Area_acre
            {
                get { return _area_acre; }
                set
                {
                    _area_acre = value;
                }
            }
            public string Area_Bigha
            {
                get { return _area_bigha; }
                set
                {
                    _area_bigha = value;
                }
            }
            public string Area_Decimal
            {
                get { return _area_decimal; }
                set
                {
                    _area_decimal = value;
                }
            }
            public string Area_Katha
            {
                get { return _area_katha; }
                set
                {
                    _area_katha = value;
                }
            }
            public string Area_Chatak
            {
                get { return _area_chatak; }
                set
                {
                    _area_chatak = value;
                }
            }
            public string Area_SqtL
            {
                get { return _area_sqtL; }
                set
                {
                    _area_sqtL = value;
                }
            }
            public string Total_decimal
            {
                get { return _total_decimal; }
                set
                {
                    _total_decimal = value;
                }
            }
            public string property_type
            {
                get { return _property_type; }
                set
                {
                    _property_type = value;
                }
            }
            public string structure_sqt
            {
                get { return _structure_sql; }
                set
                {
                    _structure_sql = value;
                }
            }
            public string local_body_type
            {
                get { return _local_body_type; }
                set
                {
                    _local_body_type = value;
                }
            }
            public string other_details
            {
                get { return _other_details; }
                set
                {
                    _other_details = value;
                }
            }
            public bool Selected
            {
                get { return _selected; }
                set
                {
                    _selected = value;
                    //this.NotifyPropertyChanged("Selected");
                }
            }
           
           
            public string Created_By
            {
                get { return _created_by; }
                set { _created_by = value; }
            }
            public string Created_Dttm
            {
                get { return _created_dttm; }
                set { _created_dttm = value; }
            }

        }

        public class DeedControl
        {
            private string _district_code;

            public string District_code
            {
                get { return _district_code; }
                set { _district_code = value; }
            }

            private string _RO_code;

            public string RO_code
            {
                get { return _RO_code; }
                set { _RO_code = value; }
            }


            private string _Book;

            public string Book
            {
                get { return _Book; }
                set { _Book = value; }
            }
            private string _Deed_year;

            public string Deed_year
            {
                get { return _Deed_year; }
                set { _Deed_year = value; }
            }
            private string _Deed_no;

            public string Deed_no
            {
                get { return _Deed_no; }
                set { _Deed_no = value.PadLeft(5, '0'); }
            }


           

            public override string ToString()
            {
                return District_code + ", " + RO_code + ", " + Book + ", " + Deed_year + ", " + Deed_no;
            }

            public string Serial_no { get; set; }
        }

        public class DeedDetails
        {
            private DeedControl _Deed_control;
            private string _Serial_no;
            private string _Serial_year;
            private string _tran_maj_code;
            private string _tran_min_code;
            private string _volume_no;
            private string _page_from;
            private string _page_to;
            private string _date_of_completion;
            private string _date_of_delivery;
            private string _deed_remarks;
            private string _doc_type;
            private string _scan_doc_type;
            private string _addl_pages;
            private string _hold;
            private string _hold_reason;
            private string _status;
            private string _created_system;
            private string _version;
            private string _exported;
            private string _mismatch;
            private string _created_by;
            private string _created_dttm;
            private string _modified_by;
            private string _modified_dttm;
            //private string _exception;

            public DeedDetails()
            {
                _Deed_control = new DeedControl();
            }
            public DeedControl Deed_control
            {
                get { return _Deed_control; }
                set { _Deed_control = value; }
            }
            public string volume_no
            {
                get { return _volume_no; }
                set
                {
                    _volume_no = value;
                }

            }
            public string page_from
            {
                get { return _page_from; }
                set
                {
                    _page_from = value;
                }
            }
            public string page_to
            {
                get { return _page_to; }
                set
                {
                    _page_to = value;
                }
            }
            public string date_of_completion
            {
                get { return _date_of_completion; }
                set
                {
                    _date_of_completion = value;
                }
            }
            public string date_of_delivery
            {
                get { return _date_of_delivery; }
                set
                {
                    _date_of_delivery = value;
                }
            }
            public string deed_remarks
            {
                get { return _deed_remarks; }
                set
                {
                    _deed_remarks = value;
                }
            }
            public string doc_type
            {
                get { return _doc_type; }
                set
                {
                    _doc_type = value;
                }
            }
            public string hold
            {
                get { return _hold; }
                set
                {
                    _hold = value;
                }
            }
            public string hold_reason
            {
                get { return _hold_reason; }
                set
                {
                    _hold_reason = value;
                }
            }
            public string Exported
            {
                get { return _exported; }
                set { _exported = value; }
            }


            public string version
            {
                get { return _version; }
                set
                {
                    _version = value;
                }
            }
            public string created_system
            {
                get { return _created_system; }
                set
                {
                    _created_system = value;
                }
            }
            public string status
            {
                get { return _status; }
                set
                {
                    _status = value;
                }
            }

            public string addl_pages
            {
                get { return _addl_pages; }
                set
                {
                    _addl_pages = value;
                }
            }

            public string scan_doc_type
            {
                get { return _scan_doc_type; }
                set
                {
                    _scan_doc_type = value;
                }
            }
            public string Serial_no
            {
                get { return _Serial_no; }
                set
                {
                    _Serial_no = value;
                }
            }
            public string Serial_year
            {
                get { return _Serial_year; }
                set
                {
                    _Serial_year = value;
                }
            }
            public string tran_maj_code
            {
                get { return _tran_maj_code; }
                set
                {
                    _tran_maj_code = value;
                }
            }
            public string tran_min_code
            {
                get { return _tran_min_code; }
                set
                {
                    _tran_min_code = value;
                }
            }
            public string Created_By
            {
                get { return _created_by; }
                set { _created_by = value; }
            }
            public string Created_Dttm
            {
                get { return _created_dttm; }
                set { _created_dttm = value; }
            }
            public string Modified_By
            {
                get { return _modified_by; }
                set { _modified_by = value; }
            }
            public string Modified_Dttm
            {
                get { return _modified_dttm; }
                set { _modified_dttm = value; }
            }
            public string Mismatch
            {
                get { return _mismatch; }
                set { _mismatch = value; }
            }
            //public string exception
            //{
            //    get { return _exception; }
            //    set { _exception = value; }
            //}
        }

        public static class Constant
        {
            public static string INI_FILE_NAME = "EDMS.INI";
            public static string INI_SECTION = "DBCon";
            public static string INI_KEY = "EDMS";

            public static string _STATUS_ON_HOLD = "12";
        }
        public class PropertyDetails_other_plot
        {
            private string _district_code;
            private string _RO_code;
            private string _Book;
            private string _Deed_year;
            private string _Deed_no;
            private string _item_no;
            private string _other_plot_no;

            public string district_code
            {
                get { return _district_code; }
                set
                {
                    _district_code = value;
                }
            }

            public string RO_code
            {
                get { return _RO_code; }
                set
                {
                    _RO_code = value;
                }
            }
            public string Book
            {
                get { return _Book; }
                set
                {
                    _Book = value;
                }
            }
            public string Deed_year
            {
                get { return _Deed_year; }
                set
                {
                    _Deed_year = value;
                }
            }
            public string Deed_no
            {
                get { return _Deed_no; }
                set
                {
                    _Deed_no = value;
                }
            }
            public string item_no
            {
                get { return _item_no; }
                set
                {
                    _item_no = value;
                }
            }
            public string other_plot_no
            {
                get { return _other_plot_no; }
                set
                {
                    _other_plot_no = value;
                }
            }
        }
        public class PropertyDetails_other_khatian
        {
            private string _district_code;
            private string _RO_code;
            private string _Book;
            private string _Deed_year;
            private string _Deed_no;
            private string _item_no;
            private string _other_Khatian_no;

            public string district_code
            {
                get { return _district_code; }
                set
                {
                    _district_code = value;
                }
            }

            public string RO_code
            {
                get { return _RO_code; }
                set
                {
                    _RO_code = value;
                }
            }
            public string Book
            {
                get { return _Book; }
                set
                {
                    _Book = value;
                }
            }
            public string Deed_year
            {
                get { return _Deed_year; }
                set
                {
                    _Deed_year = value;
                }
            }
            public string Deed_no
            {
                get { return _Deed_no; }
                set
                {
                    _Deed_no = value;
                }
            }
            public string item_no
            {
                get { return _item_no; }
                set
                {
                    _item_no = value;
                }
            }
            public string other_Khatian_no
            {
                get { return _other_Khatian_no; }
                set
                {
                    _other_Khatian_no = value;
                }
            }
        }
        public class Deed
        {
            DeedDetails deedHeader;

            private List<PropertyDetails_other_plot> _lst_other_plots;

            public List<PropertyDetails_other_plot> Lst_other_plots
            {
                get { return _lst_other_plots; }
                set { _lst_other_plots = value; }
            }
            private List<PropertyDetails_other_khatian> _lst_other_khatians;

            public List<PropertyDetails_other_khatian> Lst_other_khatians
            {
                get { return _lst_other_khatians; }
                set { _lst_other_khatians = value; }
            }
            public DeedDetails DeedHeader
            {
                get { return deedHeader; }
                set { deedHeader = value; }
            }
            private List<PropertyDetails> properties;

            public List<PropertyDetails> Properties
            {
                get { return properties; }
                set { properties = value; }
            }

            private List<PropertyDetailsWB> propertiesoutWB;

            public List<PropertyDetailsWB> PropertiesoutWB
            {
                get { return propertiesoutWB; }
                set { propertiesoutWB = value; }
            }
            private List<PersonDetails> persons;

            public List<PersonDetails> Persons
            {
                get { return persons; }
                set { persons = value; }
            }

            private List<deedDetailsException> d_excp;

            public List<deedDetailsException> D_Excp
            {
                get { return d_excp; }
                set { d_excp = value; }
            }

            private List<PersonDetailsException> p_excp;

            public List<PersonDetailsException> P_Excp
            {
                get { return p_excp; }
                set { p_excp = value; }
            }

            private List<PropertyDetailsException> pro_excp;

            public List<PropertyDetailsException> Pro_Excp
            {
                get { return pro_excp; }
                set { pro_excp = value; }
            }
            private List<outSideWBList> pro_ExcpWb;
            public List<outSideWBList> Pro_ExcpWb
            {
                get { return pro_ExcpWb; }
                set { pro_ExcpWb = value; }
            }

            public Deed()
            {
                deedHeader = new DeedDetails();
                properties = new List<PropertyDetails>();
                propertiesoutWB = new List<PropertyDetailsWB>();
                persons = new List<PersonDetails>();
                _lst_other_plots = new List<PropertyDetails_other_plot>();
                _lst_other_khatians = new List<PropertyDetails_other_khatian>();
                d_excp = new List<deedDetailsException>();
                p_excp = new List<PersonDetailsException>();
                pro_excp = new List<PropertyDetailsException>();
                pro_ExcpWb = new List<outSideWBList>();
            }
        }
        public class Volumes
        {
            private string district_Code;

            public string District_Code
            {
                get { return district_Code; }
                set { district_Code = value; }
            }

            private string district_name;

            public string District_name
            {
                get { return district_name; }
                set { district_name = value; }
            }

            private string ro_Code;

            public string Ro_Code
            {
                get { return ro_Code; }
                set { ro_Code = value; }
            }
            private string ro_name;

            public string Ro_name
            {
                get { return ro_name; }
                set { ro_name = value; }
            }

            private string year;

            public string Year
            {
                get { return year; }
                set { year = value; }
            }
            private string book;

            public string Book
            {
                get { return book; }
                set { book = value; }
            }

            private string volume;

            public string Volume
            {
                get { return volume; }
                set { volume = value; }
            }
            private string nos;

            public string Nos
            {
                get { return nos; }
                set { nos = value; }
            }

        }
    }

