<template>
  <v-container>
    <v-layout>
      <v-flex row>
        <v-card center row>
          <v-toolbar card prominent>
            <v-toolbar-title class="body-2">User Profile</v-toolbar-title>
            <v-spacer/>
            <NewConversation v-bind:contactUsername = "username"/>
            <template v-slot:extension>
              <v-tabs v-model="tab" align-with-title class="body-2">
                <v-tabs-slider color="yellow"></v-tabs-slider>

                <v-tab>Profile</v-tab>
                <v-tab v-if="isOwnUser">Edit</v-tab>
              </v-tabs>
            </template>
          </v-toolbar>
          <v-divider></v-divider>
          <v-tabs-items v-model="tab">
            <v-tab-item>
              <v-card flat>
                <v-card-text>
                  <p>{{username}}</p>
                  <p>{{name}}</p>
                  <p>{{userInfo.schoolName}} </p>
                  <p>Department: {{userInfo.departmentName}}</p>
                  <p>Member since: {{memberSince}}</p>
                  <p>Age: {{age}}</p>
                  
                </v-card-text>
              </v-card>
            </v-tab-item>
            <v-tab-item v-if="isOwnUser">
              <v-card flat>
                <!--TODO add control for logging telemetry!-->
                <v-form>
                  
                <v-container>
                  <v-layout row wrap>
                    <v-flex>
                      <v-text-field v-model="updatedUserInfo.newFirstName" label="First name" required></v-text-field>
                    </v-flex>

                    <v-flex>
                      <v-text-field v-model="updatedUserInfo.newMiddleName" label="Middle name" required></v-text-field>
                    </v-flex>

                    <v-flex>
                      <v-text-field v-model="updatedUserInfo.newLastName" label="Last name" required></v-text-field>
                    </v-flex>
                    <v-divider></v-divider>
                  </v-layout>
                  <v-layout row wrap>
                    <v-flex
                
                    pa-1
                    >
                    <v-select
                        v-model="updatedUserInfo.newDepartmentName"
                        :items="departments"
                        label="Department"
                    ></v-select>
                    </v-flex>
                  </v-layout>
                  <v-layout row wrap>
                    <v-checkbox v-model="updatedUserInfo.newAllowTelemetry" :label="`Allow telemetric data recording.`"></v-checkbox>
                  </v-layout>
                  <v-layout>
                    <v-btn @click="modifyProfile">Enter</v-btn>
                  </v-layout>
                  
                </v-container>
                </v-form>
              </v-card>
            </v-tab-item>
          </v-tabs-items>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>


<script>
import AppSession from '@/services/AppSession'
import NewConversation from '@/components/Messenger/NewConversation'
export default {
  name: "Profile",
  components:{
    NewConversation
  },
  data() {
    return {
      tab: null,
      username: "",
      userInfo: {
        firstName: "",
        middleName: "",
        lastName: "",
        schoolName: "",
        departmentName: "",
        ranking: "",
        allowTelemetry: false,
        courses: "",
      },
      memberSince: "",
      age: "",
      updatedUserInfo: {
        newFirstName: "New",
        newMiddleName: "new",
        newLastName: "New",
        newDepartmentName: "New",
        newAllowTelemetry: false
      },
      departments: null,
      viewingUserId: AppSession.state.userId,
      allowTelemetry: false
    }
  },
  methods: {
    async getUserInfo(profileUserId) {
      console.log(`Id of get user info is ${profileUserId}`)
      await this.axios.get(`${this.$hostname}UserManager/UserProfile?accountId=${profileUserId}`, 
                     {headers: {'Accept': 'application/json',
                                'Content-Type': 'application/json',
                                'Authorization': `Bearer ${AppSession.state.token}`}})
                .then(response => {
                    // Get user userid.
                    AppSession.updateSession(response.data.SITtoken)
                    this.userInfo.firstName = response.data.User.FirstName
                    this.userInfo.middleName = response.data.User.MiddleName
                    this.userInfo.lastName = response.data.User.LastName
                    this.userInfo.schoolName = response.data.User.SchoolName
                    this.userInfo.departmentName = response.data.User.DepartmentName
                    this.userInfo.allowTelemetry = response.data.User.AllowTelemetry
                    this.userInfo.courses = response.data.user.Courses
                    console.log('About to finish getUserInfo')
                })
                .catch(error => {
                  //Indicate an error. Server might be down so just logout the user.
                  // delete token to logout the user.
                  // Redirect user to error page.
                })
    },
    modifyProfile() {
      let requestPayload = {
          FirstName: this.updatedUserInfo.newFirstName,
          MiddleName: this.updatedUserInfo.newMiddleName,
          LastName: this.updatedUserInfo.newLastName,
          AllowTelemetry: this.updatedUserInfo.newAllowTelemetry,
          DepartmentName: this.updatedUserInfo.newDepartmentName
        }
      let headersObject = {
          headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + AppSession.state.token
          }
        }
      let editUrl = `${this.$hostname}/User/EditUser`
      this.axios.post(editUrl, requestPayload, headersObject)
                .then(response => {
                  console.log("starting frontend")
                  AppSession.updateSession(response.data)
                  this.getUserInfo(this.$route.params.id).then(() => {
                    if (this.isOwnUser) {
                      this.synchToInputs()
                    }
                  }).catch((error)=> {

                  })
                  console.log("Update succeeded")
                })
                .catch(error => {console.log('Error in edit')})
    },
    synchToInputs() {
      console.log('This is the first name' + this.userInfo.firstName)
      this.updatedUserInfo.newFirstName = this.userInfo.firstName
      this.updatedUserInfo.newMiddleName = this.userInfo.middleName
      this.updatedUserInfo.newLastName = this.userInfo.lastName
      this.updatedUserInfo.newDepartmentName = this.userInfo.departmentName
      this.updatedUserInfo.newAllowTelemetry = this.userInfo.allowTelemetry
      
    },
  //   getDepartments: function(){

  //       const url = `${this.$hostname}search/selections`;
  //       this.axios.get(url, {
  //           params:{
  //               SearchCategory: 1,
  //               SearchSchool: this.userInfo.schoolName
  //           },
  //           headers: { "Content-Type": "application/json", Authorization: "Bearer " + sessionStorage.SITtoken }
            
  //       })
  //       .then(response =>{
  //         // finish this.
  //         this.departments = response.data;
  //       })
  //       .catch(error =>{
  //           this.errorMessage = error.response.data.Message
  //       })
        
  //   }
  // },
  },
  computed: {
    name() {
      return this.userInfo.firstName + " " + this.userInfo.lastName;
    },
    isOwnUser() {
      if (this.$route != undefined) {
        return parseInt(this.viewingUserId) === parseInt(this.$route.params.id)
      }
      return false;
    }
  },
  watch: {
    '$route' (to,from) {
      if(to.params.id != undefined) {
        this.getUserInfo(to.params.id).then(() =>  {
          console.log('getUserInfo is complete in watch')
          if (this.isOwnUser) {
            this.synchToInputs()
            this.getDepartments()
          }
        }).catch((error)=> {

          })
      }
    }
  },
  created() {
    console.log("In created of Profile.vue")
    this.getUserInfo(this.$route.params.id)
        .then(() => {
          if (this.isOwnUser) {
            this.synchToInputs()
            this.getDepartments()
          }
        }).catch((error)=> {

          })
  }
}

</script>


