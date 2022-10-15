// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";
import {
  getDatabase,
  onValue,
  ref as storageRef,
  query as dbQuery,
} from "firebase/database";

import {
  collection,
  getDocs,
  doc,
  onSnapshot,
  orderBy,
  where,
  query,
  addDoc,
  updateDoc,
} from "firebase/firestore";

import {
  getStorage,
  ref,
  uploadBytes,
  getDownloadURL,
  uploadBytesResumable,
} from "firebase/storage";
import {
  FacebookAuthProvider,
  getAuth,
  GoogleAuthProvider,
} from "firebase/auth";
import { getFirestore } from "firebase/firestore";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
const firebaseConfig = {
  apiKey: "AIzaSyAsem870UdpZSF10dPiGwjouLb82ftVEEA",
  authDomain: "voice-project-f2a8b.firebaseapp.com",
  projectId: "voice-project-f2a8b",
  storageBucket: "voice-project-f2a8b.appspot.com",
  messagingSenderId: "605857742441",
  appId: "1:605857742441:web:b177c0818f004e3b982cf8"
};


// Initialize Firebase
const app = initializeApp(firebaseConfig);
const analytics = getAnalytics(app);

const auth = getAuth(app);
const db = getFirestore(app);
const fbProvider = new FacebookAuthProvider();
const storage = getStorage(app);
fbProvider.addScope("user_photos");
fbProvider.addScope("publicProfile");
const ggProvider = new GoogleAuthProvider();
const database = getDatabase();

export {
  auth,
  db,
  fbProvider,
  ggProvider,
  storage,
  ref,
  uploadBytes,
  getDownloadURL,
  uploadBytesResumable,
  collection,
  getDocs,
  doc,
  onSnapshot,
  orderBy,
  where,
  query,
  addDoc,
  database,
  onValue,
  storageRef,
  dbQuery,
  updateDoc,
};
