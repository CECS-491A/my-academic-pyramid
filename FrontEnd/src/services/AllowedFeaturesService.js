import * as RouteNames from "@/constants/NavLinkNames"

export default {
    NonStudent: [
        RouteNames.USERHOMEPAGE,
        RouteNames.DISCUSSIONFORUM,
        RouteNames.SEARCH
    ],
    Student: [
        RouteNames.USERHOMEPAGE,
        RouteNames.PROFILE,
        RouteNames.SEARCH,
        RouteNames.DASHBOARD, //admin
        RouteNames.CHAT,
        RouteNames.LOGGING,   //
        RouteNames.PUBLISH,
        RouteNames.DISCUSSIONFORUM
    ],
    NewUser: [
        
    ]

}