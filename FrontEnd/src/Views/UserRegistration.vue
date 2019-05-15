<template>
<v-app>
<v-form>
        <v-container>
            <v-layout row wrap>
                <v-flex>
                    <v-text-field
                      label="Solo"
                      placeholder="First Name"
                      solo
                      v-model="firstName"
                    ></v-text-field>
                </v-flex>
                <v-flex>
                    <v-text-field
                      label="Solo"
                      placeholder="Middle Name"
                      solo
                      v-model="middleName"
                    ></v-text-field>
                </v-flex>
                <v-flex>
                    <v-text-field
                      label="Solo"
                      placeholder="Last Name"
                      solo
                      v-model="lastName"
                    ></v-text-field>
                </v-flex>
            </v-layout>

            <v-layout row>
                <v-flex
                
                pa-1
                >
                <v-select
                    v-model="school"
                    :items="schools"
                    label="School"
                    @input="getDepartments"
                ></v-select>
                </v-flex>
                
                <v-flex
                
                pa-1
                >
                <v-select
                    v-model="department"
                    :items="departments"
                    label="Department of Major"
                ></v-select>
                </v-flex>
            </v-layout>
            <v-layout>
              <v-flex
                
                pa-1
                >
                <v-select
                    v-model="courseDepartment"
                    :items="departments"
                    label="Course Department"
                    @input="getCourses"
                ></v-select>
                </v-flex>
            </v-layout>
            <v-layout>
              <v-flex
                pa-1
                >
                <v-select
                  v-model="courseId"
                  :items="courses"
                  label="Course"
                  @input="getTeacherCourses"
                ></v-select>
              </v-flex>
            </v-layout>

            <v-layout>
              <v-select
                v-model="selectedTeacherCourses"
                :items="teacherCourses"
                :menu-props="{ maxHeight: '400' }"
                label="Select Course"
                multiple
                hint="Pick your favorite states"
                persistent-hint
              ></v-select>
            </v-layout>

            <v-layout row wrap>
              <v-flex xs12 sm6 md4>
                <v-menu
                  ref="menu"
                  v-model="menu"
                  :close-on-content-click="false"
                  :nudge-right="40"
                  lazy
                  transition="scale-transition"
                  offset-y
                  full-width
                  min-width="290px"
                >
                  <template v-slot:activator="{ on }">
                    <v-text-field
                      v-model="dateOfBirth"
                      label="Birthday date"
                      prepend-icon="event"
                      readonly
                      v-on="on"
                    ></v-text-field>
                  </template>
                  <v-date-picker
                    ref="picker"
                    v-model="dateOfBirth"
                    :max="new Date().toISOString().substr(0, 10)"
                    min="1950-01-01"
                    @change="save"
                  ></v-date-picker>
                </v-menu>
            </v-flex>
          </v-layout>

          <v-btn @click="registerUser" color="primary">Complete Registration</v-btn>
          <v-btn @click="logoutFunc">Log Out</v-btn>

          <v-alert
              :value="errorMessage"
              id="errorMessage"
              type="error"
              transition="scale-transition"
          >
              {{errorMessage}}
          </v-alert>
        </v-container>
    </v-form>
</v-app>
  
</template>

<script>
import Axios from 'axios';
import AppSession from '@/services/AppSession'
//import axios from 'axios'
export default {
  name: "UserRegistration",
  data () {
    return {
      firstName: "",
      middleName: "",
      lastName: "",
      userName: "",
      email: "",
      dateOfBirth: null,
      school: "",
      schools: [],
      department: "",
      departments: [],
      courseDepartment: "",
      courseId: "",
      courses: [],
      teacherCourseId: "",
      teacherCourses: [],
      selectedTeacherCourses: [],
      errorMessage: "",
      menu: false
    }
  },
  watch: {
    menu (val) {
      val && setTimeout(() => (this.$refs.picker.activePicker = 'YEAR'))
    }
  },
  methods: {
    save (date) {
      this.$refs.menu.save(date)
    },

    registerUser() {
        let requestPayload = {
          FirstName: this.firstName,
          MiddleName: this.middleName,
          LastName: this.lastName,
          DateOfBirth: this.dateOfBirth,
          SchoolId: this.school,
          DepartmentId: this.department,
          selectedCourseIds: this.selectedTeacherCourses
        }
        let headersObject = {
          headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + AppSession.state.token
          }
        }
        // console.log(requestPayload.DateOfBirth)
        let urlRegistration = `${this.$hostname}Registration`
        Axios.post(urlRegistration, requestPayload, headersObject)
             .then(response => {
               AppSession.updateSession(response.data.SITtoken)
               return this.axios.get(
                `${this.$hostname}UserManager/GetUserInfoWithId?id=${AppSession.state.userId}`, 
                {headers: {'Accept': 'application/json',
                            'Content-Type': 'application/json',
                            'Authorization': `Bearer ${AppSession.state.token}`}}
               )
               })
             .then((response) => {
                AppSession.setCategory(response.data.User.Category)
                AppSession.setSchoolId(this.school)
                AppSession.updateSession(response.data.SITtoken)
                this.$router.push({name: "UserHomePage"})})
             .catch(error =>{
                this.errorMessage = error.response.data.Message
              })
    },

    logoutFunc() {
      let requestPayload = parseInt(AppSession.state.userId)
      let headersObject = {
          headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + AppSession.state.token
          }
      }
      let urlLogOut = `${this.$hostname}Logout`
      this.axios.post(urlLogOut, requestPayload, headersObject)
                .then(response => {
                  // TODO check the status code
                  AppSession.logout()
                  this.$router.push({name: "Home"})
                })
                .catch(error => {

                })
    },
    getSchools: function(){
        this.errorMessage = "";

        const url = `${this.$hostname}search/selections`;
        Axios
        .get(url, {
            params:{
                SearchCategory: 0
            },
            headers: { "Content-Type": "application/json", Authorization: "Bearer " + sessionStorage.SITtoken }
            
        })
        .then(response =>{
            this.schools = response.data;
            if(this.schools.length > 0){
                this.schools = [{id: 0, text: "NONE", value: 0 }].concat(this.schools)
            }
        })
        .catch(error =>{
            this.errorMessage = error.response.data.Message
        })
        
    },
    getDepartments: function(){
      this.errorMessage = "";

      const url = `${this.$hostname}search/selections`;
      Axios
      .get(url, {
          params:{
              SearchCategory: 1,
              SearchSchool: this.school
          },
          headers: { "Content-Type": "application/json", Authorization: "Bearer " + sessionStorage.SITtoken }
          
      })
      .then(response =>{
          this.departments = response.data;
      })
      .catch(error =>{
          this.errorMessage = error.response.data.Message
      })   
    },
    getCourses: function() {
      this.errorMessage = "";

        const url = `${this.$hostname}search/selections`;
        Axios
        .get(url, {
            params:{
                SearchCategory: 2,
                SearchSchool: this.school,
                SearchDepartment: this.courseDepartment
            },
            headers: { "Content-Type": "application/json", Authorization: "Bearer " + sessionStorage.SITtoken }
            
        })
        .then(response =>{
            this.courses = response.data;
        })
        .catch(error =>{
            this.errorMessage = error.response.data.Message
        })  
    },
    getTeacherCourses: function() {
      this.errorMessage = "";

        const url = `${this.$hostname}search/selections`;
        Axios
        .get(url, {
            params:{
                SearchCategory: 3,
                SearchSchool: this.school,
                SearchDepartment: this.courseDepartment,
                SearchCourse: this.courseId
            },
            headers: { "Content-Type": "application/json", Authorization: "Bearer " + sessionStorage.SITtoken }
            
        })
        .then(response =>{
            this.teacherCourses = response.data;
        })
        .catch(error =>{
            this.errorMessage = error.response.data.Message
        })  
    }
  },

  created() {        
      console.log("In creation")
      // try to get userinfo
      this.axios.get(`${this.$hostname}UserManager/GetContextId`, 
                     {headers: {'Accept': 'application/json',
                                'Content-Type': 'application/json',
                                'Authorization': `Bearer ${AppSession.state.token}`}})
                .then(response => {
                    // Get user userid.
                    AppSession.setUserId(response.data.userid)
                    AppSession.updateSession(response.data.SITtoken)
                })
                .catch(error => {
                  //Indicate an error. Server might be down so just logout the user.
                  // delete token to logout the user.
                  // Redirect user to error page.
                })
  },
  beforeMount(){
        this.getSchools()
    },
}
</script>