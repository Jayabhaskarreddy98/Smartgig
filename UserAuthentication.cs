using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using System.Security.Permissions;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Configuration;
using InfionicCommonServices;
using Infionic.Entities;
using log4net;

namespace InfionicCommonServices
{
    [DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
    public class UserAuthentication
    {
        #region Fields
        //Ldap connection properties
        private static readonly ILog logger = LogManager.GetLogger(typeof(UserAuthentication));
        private LdapConnection ldapConnection = null;
        private NetworkCredential credential;
        private CommonAccountService _accountSrv;
        #endregion

        #region Properties
        public CommonAccountService AccountSrv
        {
            get
            {
                if (_accountSrv == null)
                {
                    _accountSrv = new CommonAccountService();
                }
                return _accountSrv;
            }
        }
        #endregion

        #region Methods
        private bool CreateLdapConnection()
        {
            try
            {
                credential = new NetworkCredential(ConfigurationManager.AppSettings["Userid"], ConfigurationManager.AppSettings["Password"]);
                // Creating the new LDAP connection
                ldapConnection = new LdapConnection(ConfigurationManager.AppSettings["Server"]);
                ldapConnection.Credential = credential;
                ldapConnection.AuthType = AuthType.Basic;
                //ldapConnection.Bind();
                return true;
            }
            catch (LdapException le)
            {
                throw le;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User Authenticate(string userName, string password, string authenticationMode, int topMenuParentId, int companyId, bool? isLoginLocked = false)
        {
            User DBUser = null;
            string status = string.Empty;
            try
            {
                int userId = 0;
                //if isLDAPAuthentication is ture we need to check against ldapserver else database
                if (authenticationMode == "LotusLDAP")
                {
                    bool conStatus = CreateLdapConnection();
                    if (conStatus)
                    {
                        //+"(mail=" + userName + "))"
                        SearchRequest request = new SearchRequest(ConfigurationManager.AppSettings["DistinguishedName"], "(&(objectClass=Person)" + string.Format(ConfigurationManager.AppSettings["LDAPSearchQuery"], userName) + ")", System.DirectoryServices.Protocols.SearchScope.Subtree);
                        SearchResponse response = (SearchResponse)ldapConnection.SendRequest(request);
                        SearchResultEntry entry = response.Entries[0];
                        string dn = entry.DistinguishedName;
                        ldapConnection.Credential = new NetworkCredential(dn, password);
                        ldapConnection.Bind();
                        status = "Success";

                        //After successfull connection we are sending username to our database to get userId.
                        userId = AccountSrv.GetUserIdByUserName(userName);
                        DBUser = AccountSrv.GetUserDetails(userId, topMenuParentId);
                    }
                }
                else
                {
                    userId = AccountSrv.AuthenticateUser(userName, password, companyId, isLoginLocked);
                    User user = AccountSrv.GetUserPasswordDetails(userId);
                    string hashPassword = PasswordModel.GetHashValue(password, user.PasswordSalt);
                    if ((user.IsLoginLocked == true ||  user.Password != hashPassword))
                    {
                        //passwords not match.we manually rising error
                        throw new DirectoryOperationException();
                    }
                    DBUser = AccountSrv.GetUserDetails(userId, topMenuParentId);
                }

                return DBUser;
            }
            catch (ArgumentOutOfRangeException aore)
            {
                if (status == "Success")
                {
                    throw new ArgumentOutOfRangeException("Error occured while getting user details");
                }
                else
                {
                    throw aore;
                }
            }
            catch (DirectoryOperationException dpe)
            {
                throw dpe;
            }
            catch (LdapException le)
            {
                throw le;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<User> SearchUserForLotusLdap(string searchText, string searchDirectoryFormat, string distinguishedName, string server, NetworkCredential credential)
        {
            try
            {
                List<User> lstUsers = new List<User>();
                ldapConnection = new LdapConnection(server);
                ldapConnection.Credential = credential;
                ldapConnection.AuthType = AuthType.Basic;
                SearchRequest request = new SearchRequest(distinguishedName, string.Format(searchDirectoryFormat, searchText), System.DirectoryServices.Protocols.SearchScope.Subtree);
                SearchResponse response = (SearchResponse)ldapConnection.SendRequest(request);

                for (int i = 0; i < response.Entries.Count; i++)
                {
                    User usr = new User();
                    if (response.Entries[i].Attributes.Contains("uid"))
                    {
                        usr.Uid = response.Entries[i].Attributes["uid"][0].ToString();
                    }
                    if (response.Entries[i].Attributes.Contains("mail"))
                    {
                        usr.Mail = response.Entries[i].Attributes["mail"][0].ToString();
                    }
                    if (response.Entries[i].Attributes.Contains("cn"))
                    {
                        usr.Cn = response.Entries[i].Attributes["cn"][0].ToString();
                    }
                    if (response.Entries[i].Attributes.Contains("cn"))
                    {
                        usr.Displayname = response.Entries[i].Attributes["displayname"][0].ToString();
                    }

                    lstUsers.Add(usr);
                }

                return lstUsers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region TokenAuthentication
        public User AuthenticateToken(string userName, string token)
        {
            User DBUser = null;
            try
            {
                DBUser = AccountSrv.GetUserDetailsForTokenAuth(userName, token);
                return DBUser;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                throw ex;
            }

        }
        #endregion
    }
}