import React, { useEffect, useRef, useState } from 'react'
import { Link } from 'react-router-dom'
import { Modal, Radio } from 'antd'
import './profilemini.style.scss'
import { getAuthen, postAuthen } from '../../axios/authenfunction'
import API from '../../constans/api'
import Swal from 'sweetalert2'
import Rating from '@mui/material/Rating'

function ProfileMini({ data, isShowInvite }) {
  const onChange = e => {
    setValue(e.target.value)
  }
  let [isShowAddJob, setIsShowAddJob] = useState(false)
  const [value, setValue] = useState(1)
  let [prepareData, setPrepareData] = useState({
    sub: [],
    demos: [],
    account: [],
    jobs: []
  })
  let title2 = useRef(null)
  let description2 = useRef(null)
  let day = useRef(null)
  let hours = useRef(null)
  let minute = useRef(null)
  let price = useRef(null)
  let tone = useRef(null)
  let minAge = useRef(null)
  let maxAge = useRef(null)
  let gender = useRef(null)
  let language = useRef(null)

  useEffect(() => {
    getAuthen(API['GET_SUBCATEGORY']).then(response => {
      setPrepareData({
        sub: response.data.data
      })
    })
  }, [])

  const handelAddJob = () => {
    const txtTitle = title2.current.value
    const txtDescription = description2.current.value
    const txtDay = day.current.value
    const txtHours = hours.current.value
    const txtMinute = minute.current.value
    const txtPrice = price.current.value
    const txtTone = tone.current.value
    const txtMinAge = minAge.current.value
    const txtMaxAge = maxAge.current.value
    const txtGender = gender.current.value
    const txtLanguage = language.current.value

    const dataa = {
      jobPayload: {
        name: txtTitle,
        description: txtDescription,
        dayDuration: new Number(txtDay),
        hourDuration: new Number(txtHours),
        minuteDuration: new Number(txtMinute),
        subCategoryId: value,
        price: new Number(txtPrice),
        tone: new Number(txtTone),
        minAge: new Number(txtMinAge),
        maxAge: new Number(txtMaxAge),
        gender: new Number(txtGender),
        language: new Number(txtLanguage)
      },
      candidateId: data.id
    }

    postAuthen(API['INVITE_TO_JOB'], dataa, true)
      .then(response => {
        setIsShowAddJob(false)
        Swal.fire('Th??ng b??o', 'M???i th??nh c??ng!', 'success')
      })
      .catch(error => {
        Swal.fire({
          icon: 'error',
          title: 'Oops...',
          text: error.response.data.msg
        })
      })
  }

  const createSchedule = async () => {
    const { value: formValues } = await Swal.fire({
      title: 'B??o c??o ???ng c??? vi??n',
      html: `
              <input type="datetime-local"  id="swal-input1">
        `,
      showCancelButton: true,
      focusConfirm: false,
      confirmButtonText: 'T???o l???ch h???n',
      preConfirm: () => {
        return {
          candidateId: data.id,
          scheduledTime: document.getElementById('swal-input1').value
        }
      }
    })

    if (formValues) {
      postAuthen(API['POST_SCHEDULE'], formValues, true)
        .then(response => {
          Swal.fire(
            'Th??ng b??o!',
            'T???o l???ch h???n v???i ???ng vi??n th??nh c??ng',
            'success'
          )
        })
        .catch(error => {
          console.log(error)
          Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: '?????t l???ch kh??ng th??nh c??ng!'
          })
        })
    }
  }
  return (
    <div className="profile-mini box">
      <div className="left">
        <img src={data.avatarUrl} alt="" />
      </div>
      <div className="right">
        <Link to={`/profile/${data.id}`} target="_blank">
          <h3>{data.name}</h3>
        </Link>
        <p>{data.description}</p>
        <Rating name="read-only" value={data.averageReviewPoint} readOnly />
        {isShowInvite ? (
          <div className="wrapper">
            <button
              className="button"
              onClick={() => {
                setIsShowAddJob(true)
              }}
            >
              M???i v??o d??? ??n
            </button>

            {/* <button
              className="button"
              style={{ marginLeft: "10px" }}
              onClick={() => {
                createSchedule();
              }}
            >
              H???n l???ch
            </button> */}
          </div>
        ) : (
          ''
        )}
      </div>
      <Modal
        title="M???i v??o c??ng vi???c"
        centered
        visible={isShowAddJob}
        onOk={() => handelAddJob()}
        onCancel={() => setIsShowAddJob(false)}
        width={800}
        className="modal-adddemo"
      >
        <div className="modal-adddemo__skill box">
          <h3>
            K?? n??ng<span>*</span>
          </h3>
          <p>Ch???n 1 k?? n??ng ph?? h???p khi m???i ???ng vi??n tham gia c??ng vi???c.</p>
          <div className="modal-adddemo__skill__list">
            <Radio.Group onChange={onChange} value={value}>
              {prepareData.sub.length > 0
                ? prepareData.sub.map((item, index) => {
                    return (
                      <Radio value={item.id} key={index}>
                        {item.name}
                      </Radio>
                    )
                  })
                : ''}
            </Radio.Group>
          </div>
          <h3>
            Ng??n ng??? y??u c???u cho ???ng vi??n <span>*</span>
          </h3>
          <select ref={language}>
            <option disabled selected value>
              -- Ch???n --
            </option>
            <option value="0">Ti???ng Vi???t</option>
            <option value="1">Ti???ng Anh</option>
          </select>
          <h3>
            Gi???i t??nh y??u c???u cho ???ng vi??n <span>*</span>
          </h3>
          <select ref={gender}>
            <option disabled selected value>
              -- Ch???n --
            </option>
            <option value="0">Nam</option>
            <option value="1">N???</option>
          </select>
          <h3>
            Tone <span>*</span>
          </h3>
          <select ref={tone}>
            <option disabled selected value>
              -- Ch???n --
            </option>
            <option value="0">Gi???ng tr???m</option>
            <option value="1">Gi???ng v???a</option>
            <option value="2">Gi???ng cao</option>
          </select>
        </div>
        <div className="box">
          <div className="row">
            <h3>
              ????? tu???i <span>*</span>
            </h3>
            <input type="number" min="18" ref={minAge} /> -{' '}
            <input type="number" min="18" ref={maxAge} />
          </div>
        </div>
        <div className="modal-adddemo__description box">
          <h3>
            Ti??u ????? <span>*</span>
          </h3>
          <p>Mi??u t??? ng???n g???n ti??u ????? c???n cho c??ng vi???c c???a b???n.</p>
          <input type="text" ref={title2} />
          <h3>
            N???i dung <span>*</span>
          </h3>
          <p>Chia s??? m???t s??? y??u c???u c??ng vi???c c???n thi???t cho ???ng vi??n.</p>
          <input type="text" ref={description2} />
        </div>

        <div className="box">
          <div className="row">
            <h3>
              Ng??y ho??n th??nh<span>*</span>
            </h3>
            <input type="number" ref={day} />
          </div>
          <div className="row">
            <h3>
              Gi??? ho??n th??nh <span></span>
            </h3>
            <input type="number" ref={hours} />
          </div>
          <div className="row">
            <h3>
              Ph??t ho??n th??nh <span></span>
            </h3>
            <input type="number" ref={minute} />
          </div>
          <div className="row">
            <h3>
              Gi?? <span>*</span>
            </h3>
            <input type="number" ref={price} />
          </div>
        </div>
      </Modal>
    </div>
  )
}

export default ProfileMini
