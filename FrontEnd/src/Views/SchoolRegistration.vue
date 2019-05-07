
<template>
  <div id="schoolRegistration">
    <v-app id="schoolRegistration">
      <v-card>
        <v-toolbar>
          <v-toolbar-title>Create User</v-toolbar-title>
        </v-toolbar>
        <v-tabs left color="cyan" dark icons-and-text>
          <v-tabs-slider color="green"></v-tabs-slider>
          <v-tab id="JSONfile">File Upload</v-tab>
          <v-tab-item>
            _
            <v-card-title primary-title>
              <div>
                <h3 class="headline mb-0">File Content</h3>
                <div>{{ schoolData.schoolDTO.SchoolName }}</div>
              </div>
            </v-card-title>

            <FileReader @load="text = $event"></FileReader>
            {{schoolData.schoolDTO.SchoolName}}
          </v-tab-item>
          <v-tab id="ManualEnter">Enter manually</v-tab>
          <v-tab-item>
            <v-card-text>
              <form>
                <v-text-field
                  id="schoolName"
                  v-model="schoolData.schoolDTO.SchoolName"
                  :error-messages="textErrors"
                  label="School Name"
                  required
                  @input="$v.schoolData.schoolDTO.SchoolName.$touch()"
                  @blur="$v.schoolData.schoolDTO.SchoolName.$touch()"
                ></v-text-field>

                <v-text-field
                  id="schoolContactEmail"
                  v-model="schoolData.schoolDTO.SchoolContactEmail"
                  :error-messages="emailErrors"
                  label="School Contact Email"
                  required
                  @input="$v.schoolData.schoolDTO.SchoolContactEmail.$touch()"
                  @blur="$v.schoolData.schoolDTO.SchoolContactEmail.$touch()"
                ></v-text-field>

                <v-text-field
                  id="schoolEmailDomain"
                  v-model="schoolData.schoolDTO.SchoolEmailDomain"
                  label="School Email Domain"
                  required
                  @input="$v.schoolData.schoolDTO.SchoolEmailDomain.$touch()"
                  @blur="$v.schoolData.schoolDTO.SchoolEmailDomain.$touch()"
                ></v-text-field>

                <div v-for="item in schoolData.teachers" :key="item.FirstName">
                  {{item.FirstName}}
                  {{item.LastName}}
                  {{item.MiddleName}}
                  {{item.DepartmentName}}
                </div>

                <v-btn @click="RegisterSchool">submit</v-btn>
                <v-alert
                  :value="failedAlert"
                  type="error"
                  transition="scale-transition"
                >Submit Failed. Please check all fields</v-alert>

                <v-alert
                  :value="successAlert"
                  type="success"
                  transition="scale-transition"
                >Submit Sucessfully</v-alert>

                <v-btn @click="importData">Import Data</v-btn>
              </form>
            </v-card-text>
            <courseTable></courseTable>
            <teacherTable></teacherTable>
          </v-tab-item>
        </v-tabs>
      </v-card>
    </v-app>
  </div>
</template>



<script>
/*global console*/ /* eslint no-console: "off" */
import { validationMixin } from "vuelidate";

import { required, maxLength, email } from "vuelidate/lib/validators";
import courseTable from "@/components/SchoolRegistration/CourseTable";
import teacherTable from "@/components/SchoolRegistration/TeacherTable";
import FileReader from "@/components/SchoolRegistration/FileReader";

export default {
  mixins: [validationMixin],

  validations: {
    schoolData: {
      schoolDTO: {
        SchoolName: { required, maxLength: maxLength(10) },
        SchoolContactEmail: { required, email }
      }
    }
  },
  components: {
    courseTable,
    teacherTable,
    FileReader
  },

  data: () => ({
    schoolData: {
      schoolDTO: {
        SchoolName: "",
        SchoolContactEmail: "",
        SchoolEmailDomain: ""
      },

      departmentDTO: [
        {
          DepartmentName: ""
        }
      ],

      CourseDTO: [
        {
          CourseName: "",
          SchoolName: "",
          DepartmentName: "",
          TeacherFirstName: "",
          TeacherLastName: ""
        }
      ],

      TeacherDTO: [
        {
          FirstName: "",
          MiddleName: "",
          LastName: "",
          DepartmentName: ""
        }
      ]
    },

    text: ""
  }),

  computed: {
    textErrors() {
      const errors = [];

      if (!this.$v.schoolData.schoolDTO.SchoolName.$dirty) return errors;

      !this.$v.schoolData.schoolDTO.SchoolName.maxLength &&
        errors.push("Name must be at most 10 characters long");

      !this.$v.schoolData.schoolDTO.SchoolName.required &&
        errors.push("Last Name is required.");

      return errors;
    },

    emailErrors() {
      const errors = [];

      if (!this.$v.schoolData.schoolDTO.SchoolContactEmail.$dirty)
        return errors;

      !this.$v.schoolData.schoolDTO.SchoolContactEmail.email &&
        errors.push("Must be valid e-mail");

      !this.$v.schoolData.schoolDTO.SchoolContactEmail.required &&
        errors.push("E-mail is required");

      return errors;
    }
  },
  created() {
    this.$eventBus.$on("SendCourseTable", courses => {
      this.schoolData.courses = courses;
      /* eslint no-console: "off" */
      console.log("Receive crouse");
    }),
      this.$eventBus.$on("SendTeacherTable", teachers => {
        this.schoolData.teachers = teachers;
      });
  },
  watch: {
    schoolData() {
      this.$eventBus.$emit("TeacherTableFromFile", this.schoolData.TeacherDTO),
        this.$eventBus.$emit(
          "DepartmentTableFromFile",
          this.schoolData.departmentDTO
        ),
        this.$eventBus.$emit("CourseTableFromFile", this.schoolData.CourseDTO),
        this.$eventBus.$emit(
          "TeacherTableFromFile",
          this.schoolData.TeacherDTO
        );
    }
  },
  methods: {
    submitData() {},
    convertData() {
      this.schoolData = JSON.parse(this.text);
    },
    importData() {
      this.schoolData = JSON.parse(this.text);
    },
    RegisterSchool() {
      this.axios({
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.SITtoken
        },
        method: "POST",
        crossDomain: true,
        url: this.$hostname + "SchoolRegistration",
        data: this.schoolData
      });
    }
  }
};
</script>

