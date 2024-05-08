using Microsoft.IdentityModel.Tokens;
using proveedoresAPP.Enums;
using System.Runtime.CompilerServices;

//															//AUTHOR: Towa (EFA - Eliakim Flores).
//															//CO-AUTHOR: Towa ().
//															//DATE: May 7, 2024.

namespace proveedoresAPP.Helpers
{
	//==================================================================================================================
	public static class CustomLoggerExtensions
	{
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------
        /*CONSTANTS*/
        private const string DefaultBorder = "\n\t-------------------------------------------------------------------------------" +
                                          "----------------------------------------";

        //--------------------------------------------------------------------------------------------------------------
        /*INITIALIZER*/

        //--------------------------------------------------------------------------------------------------------------
        /*INSTANCE VARIABLES*/

        //--------------------------------------------------------------------------------------------------------------
        /*COMPUTED VARIABLES*/

        //--------------------------------------------------------------------------------------------------------------
        /*METHODS TO SUPPORT COMPUTED VARIABLES*/

        //--------------------------------------------------------------------------------------------------------------
        /*OBJECT CONSTRUCTORS*/

        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------
        /*METHODS TO SUPPORT CONSTRUCTRS*/

        //--------------------------------------------------------------------------------------------------------------
        /*ACCESS METHODS*/

        //--------------------------------------------------------------------------------------------------------------
        /*TRANSFORMATION METHODS*/

        /*TASK Service methods*/
        //--------------------------------------------------------------------------------------------------------------

        //--------------------------------------------------------------------------------------------------------------
        public static void LogInformationWrapped(
            this ILogger logger_I,
            BorderEnum BorderEnum_I,
            Type classType_I,
            string strMessage_I,
            string? strBorderCharacter_I = " ",
            [CallerMemberName] string methodName = ""
            )
        {
            //                                              //Create border or use default
            string strBorder = strBorderCharacter_I.IsNullOrEmpty() ? DefaultBorder : CreateBorder(strBorderCharacter_I);

            subWriteBorderInLogInformation(logger_I, strBorder, BorderEnum_I, classType_I, strMessage_I, methodName);
        }

        //--------------------------------------------------------------------------------------------------------------
        private static void subWriteBorderInLogInformation(
            this ILogger logger,
            string Border,
            BorderEnum BorderEnum,
            Type classType,
            string message,
            [CallerMemberName] string methodName = ""
            )
        {

            if (
                BorderEnum.BorderUp == BorderEnum
                )
            {
                logger.LogInformation($"{Border}\n\t[{classType.Name} - {methodName}] - {message}\n");
            }
            else if (
                BorderEnum.BorderDown == BorderEnum
                )
            {
                logger.LogInformation($"\n\t[{classType.Name} - {methodName}] - {message}{Border}\n");
            }
            else if (
                BorderEnum.BorderBoth == BorderEnum
                )
            {
                logger.LogInformation($"{Border}\n\t[{classType.Name} - {methodName}] - {message}{Border}\n");
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void LogErrorWrapped(
            this ILogger logger_I,
            BorderEnum BorderEnum_I,
            Type classType_I,
            string strMessage_I,
            string? strBorderCharacter_I = " ",
            [CallerMemberName] string methodName = ""
            )
        {
            //                                              //Create border or use default
            string strBorder = strBorderCharacter_I.IsNullOrEmpty() ? DefaultBorder : CreateBorder(strBorderCharacter_I);

            subWriteBorderInLogError(logger_I, strBorder, BorderEnum_I, classType_I, strMessage_I, methodName);
        }

        //--------------------------------------------------------------------------------------------------------------
        private static void subWriteBorderInLogError(
            this ILogger logger,
            string Border,
            BorderEnum BorderEnum,
            Type classType,
            string message,
            [CallerMemberName] string methodName = ""
            )
        {

            if (
                BorderEnum.BorderUp == BorderEnum
                )
            {
                logger.LogError($"{Border}\n\t[{classType.Name} - {methodName}] - {message}\n");
            }
            else if (
                BorderEnum.BorderDown == BorderEnum
                )
            {
                logger.LogError($"\n\t[{classType.Name} - {methodName}] - {message}{Border}\n");
            }
            else if (
                BorderEnum.BorderBoth == BorderEnum
                )
            {
                logger.LogError($"{Border}\n\t[{classType.Name} - {methodName}] - {message}{Border}\n");
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void LogWarningWrapped(
            this ILogger logger_I,
            BorderEnum BorderEnum_I,
            Type classType_I,
            string strMessage_I,
            string? strBorderCharacter_I = " ",
            [CallerMemberName] string methodName = ""
            )
        {
            //                                              //Create border or use default
            string strBorder = strBorderCharacter_I.IsNullOrEmpty() ? DefaultBorder : CreateBorder(strBorderCharacter_I);

            subWriteBorderInLogWarning(logger_I, strBorder, BorderEnum_I, classType_I, strMessage_I, methodName);
        }

        //--------------------------------------------------------------------------------------------------------------
        private static void subWriteBorderInLogWarning(
            this ILogger logger,
            string Border,
            BorderEnum BorderEnum,
            Type classType,
            string message,
            [CallerMemberName] string methodName = ""
            )
        {

            if (
                BorderEnum.BorderUp == BorderEnum
                )
            {
                logger.LogWarning($"{Border}\n\t[{classType.Name} - {methodName}] - {message}\n");
            }
            else if (
                BorderEnum.BorderDown == BorderEnum
                )
            {
                logger.LogWarning($"\n\t[{classType.Name} - {methodName}] - {message}{Border}\n");
            }
            else if (
                BorderEnum.BorderBoth == BorderEnum
                )
            {
                logger.LogWarning($"{Border}\n\t[{classType.Name} - {methodName}] - {message}{Border}\n");
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        public static void LogDebugWrapped(
            this ILogger logger_I,
            BorderEnum BorderEnum_I,
            Type classType_I,
            string strMessage_I,
            string? strBorderCharacter_I = " ",
            [CallerMemberName] string methodName = ""
            )
        {
            //                                              //Create border or use default
            string strBorder = strBorderCharacter_I.IsNullOrEmpty() ? DefaultBorder : CreateBorder(strBorderCharacter_I);

            subWriteBorderInLogDebug(logger_I, strBorder, BorderEnum_I, classType_I, strMessage_I, methodName);
        }

        //--------------------------------------------------------------------------------------------------------------
        private static void subWriteBorderInLogDebug(
            this ILogger logger,
            string Border,
            BorderEnum BorderEnum,
            Type classType,
            string message,
            [CallerMemberName] string methodName = ""
            )
        {

            if (
                BorderEnum.BorderUp == BorderEnum
                )
            {
                logger.LogDebug($"{Border}\n\t[{classType.Name} - {methodName}] - {message}\n");
            }
            else if (
                BorderEnum.BorderDown == BorderEnum
                )
            {
                logger.LogDebug($"\n\t[{classType.Name} - {methodName}] - {message}{Border}\n");
            }
            else if (
                BorderEnum.BorderBoth == BorderEnum
                )
            {
                logger.LogDebug($"{Border}\n\t[{classType.Name} - {methodName}] - {message}{Border}\n");
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        private static string CreateBorder(string borderCharacter)
        {
            return $"\n\t{new string(borderCharacter[0], 120)}";
        }
        //--------------------------------------------------------------------------------------------------------------
        /*END-TASK*/

        //--------------------------------------------------------------------------------------------------------------
    }
    //==================================================================================================================
}
/*END-TASK*/
